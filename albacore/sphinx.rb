require 'albacore/albacoretask'
require File.join(File.expand_path(File.dirname(__FILE__)), 'config', 'sphinxconfig.rb')

class Sphinx
    include Albacore::Task
    include Albacore::RunCommand
    include SphinxConfig

    attr_accessor :sphinxbuild_script   # if your sphinx-build is a script, set this to its full path.
                                        # The python command will be used to execute the script.

    attr_accessor :pdflatex_command, :pdflatex_opts, :makeindex_command
    attr_accessor :hhc_command


    # See http://sphinx.pocoo.org/invocation.html#invocation-of-sphinx-build for details
    #   Things that are different here:
    #      -E and -a are combined into the :force option
    attr_accessor :sourcedir, :builddir
    attr_accessor :builder, :force, :doctreedir, :configdir, :nitpick, :quiet, :log_file, :warn_is_error
    attr_array :tags, :filenames
    attr_hash :define, :substitutions



    def initialize
        @command = "sphinx-build"
        @hhc_command = "hhc"
        @pdflatex_command = "pdftex"
        @makeindex_command = "makeindex"

        @pdflatex_opts = ENV['LATEXOPTS'] || ""
        @pdflatex_opts += " -progname=pdflatex"

        @sourcedir = "."
        @builddir = ".build"

        @builder = :html
        @tags = []
        @filenames = []
        @define = {}
        @substitutions = {}

        super()
        update_attributes Albacore.configuration.sphinx.to_hash
    end

    def execute
        run_sphinx()
    end

    def run_sphinx()

        check_args

        if @doctreedir == nil
            @doctreedir = "#{@builddir}/doctrees"
        end

        command_parameters = []

        if @sphinxbuild_script != nil
            @command = "python"
            command_parameters << @sphinxbuild_script
        end

        command_parameters << "-a -E" if @force == true
        command_parameters << "-n" if @nitpick == true
        command_parameters << "-q" if @quiet == true
        command_parameters << "-W" if @warn_is_error == true

        command_parameters << "-w \"#{@log_file}\"" if @log_file != nil
        command_parameters << "-d \"#{@doctreedir}\"" if @doctreedir != nil
        command_parameters << "-c \"#{@configdir}\"" if @configdir != nil


        command_parameters << build_define if @define != nil
        command_parameters << build_substitutions if @substitutions != nil
        command_parameters << build_tags if @tags != nil


        makepdf = false
        if @builder == :latexpdf
            @builder = :latex
            makepdf = true
        end

        command_parameters << "-b #{@builder.to_s}"
        command_parameters << "\"#{@sourcedir}\""
        command_parameters << "\"#{@builddir}/#{@builder.to_s}\""
        command_parameters << build_filenames if @filenames != nil

        @logger.debug command_parameters.join(" ")

        result = run_command "Sphinx-Build", command_parameters.join(" ")

        failure_message = "Sphinx Failed. See Log For Detail. Exit code: #{result}"
        fail_with_message failure_message if !result

        if makepdf == true
            @loggger.debug "Running pdflatex on output."
            run_pdflatex
        elsif @builder == :htmlhelp
            @logger.debug "Running htmlhelp on output."
            run_htmlhelp
        end
    end


    def run_htmlhelp
        Dir.mktmpdir { |tmp|
            FileUtils.cp_r "#{@builddir}/htmlhelp/.", tmp
            hhpFile = FileList["#{tmp}/*.hhp"].first
            fail_with_message "HTML help compilation failed. Unable to find the .hhp file" if hhpFile == nil
            hhpFile = hhpFile.gsub(/\//, "\\")

            @command = @hhc_command
            result = run_command "HTML Help Compiler", "\"#{hhpFile}\""

            chmFiles = FileList["#{tmp}/*.chm"]
            fail_with_message "HTML help compilation failed. No output was found." if chmFiles.length <= 0
            FileUtils.cp_r chmFiles, "#{@builddir}/htmlhelp/"
        }
    end

    def run_pdflatex
        texfiles = FileList["#{@builddir}/latex/*.tex"]

        @working_directory = "#{@builddir}/latex"
        texfiles.each { |f|
            f = File.basename(f, ".tex")

            @command = @pdflatex_command
            result = run_command "PDFTeX",  "#{@pdflatex_opts} \"#{f}\""
            fail_with_message "PDFTeX command failed. Pass 1." if !result
            result = run_command "PDFTeX",  "#{@pdflatex_opts} \"#{f}\""
            fail_with_message "PDFTeX command failed. Pass 2." if !result
            result = run_command "PDFTeX",  "#{@pdflatex_opts} \"#{f}\""
            fail_with_message "PDFTeX command failed. Pass 3." if !result

            @command = @makeindex_command
            result = run_command "MakeIndex", "-s \"python.ist\"  \"#{f}.idx\""
            fail_with_message "MakeIndex command failed." if !result

            @command = @pdflatex_command
            result = run_command "PDFTeX",  "#{@pdflatex_opts} \"#{f}\""
            fail_with_message "PDFTeX command failed. Pass 4." if !result
            result = run_command "PDFTeX",  "#{@pdflatex_opts} \"#{f}\""
            fail_with_message "PDFTeX command failed. Pass 5." if !result
        }
    end

    def check_args
        valid_builders = [:html, :dirhtml, :singlehtml, :htmlhelp, :qthelp, :devhelp, :epub, :latex, :latexpdf, :man, :texinfo, :text, :gettext, :doctest, :linkcheck]

        unless valid_builders.include?(@builder)
            @logger.warn "The specified builder is not a known one. The build may fail."
        end

        if @sourcedir == nil
            fail_with_message "Sourcedir cannot be empty"
        end

        if @builddir == nil
            fail_with_message "Builddir cannot be empty"
        end
    end

    def build_define
        option_text = []
        @define.each { |key, value| option_text << "-D #{key}\=\"#{value}\"" }
        option_text.join(" ")
    end

    def build_substitutions
        option_text = []
        @substitutions.each do |key, value|
            option_text << "-A #{key}\=\"#{value}\""
        end
        option_text.join(" ")
    end

    def build_tags
        option_text = []
        @tags.each do |key|
            option_text << "-t #{key}"
        end
        option_text.join(" ")
    end

    def build_filenames
        option_text = []
        @filenames.each do |key|
            option_text << "\"#{key}\""
        end
        option_text.join(" ")
    end
end

