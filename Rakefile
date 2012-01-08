PRODUCT       = "SMOz"
PRODUCT_LONG  = "SMOz (Start Menu Organizer)"
DESCRIPTION   = "Start Menu Organizer"
VERSION       = "2.0.0" # We omit the build segment of the version number.
AUTHORS       = "Nithin Philips"
COPYRIGHT     = "(c) 2004-2011 #{AUTHORS}"
TRADEMARKS    = "Windows is a trademark of Microsoft Corporation"

CONFIGURATION = "Release"
SOLUTION_FILE = "SMOz.sln"

BUILD_DIR     = File.expand_path("build")
OUTPUT_DIR    = "#{BUILD_DIR}/out"
BIN_DIR       = "#{BUILD_DIR}/bin"
SRC_DIR       = "#{BUILD_DIR}/src"
PACKAGES_DIR  = "packages"

PACKAGE       = "#{PRODUCT}-#{VERSION}"
BIN_PACKAGE   = "#{PACKAGE}-bin"
SRC_PACKAGE   = "#{PACKAGE}-src"
INS_PACKAGE   = "#{PACKAGE}-setup"

require 'albacore'
FileList["./albacore/*.rb"].each { |f| require f }
require 'rgl/dot'
require 'rgl/implicit'
require 'zip/zip'
require 'zip/zipfilesystem'

desc "Runs the dist task"
task :default => [:dist]

desc "Builds the application, installer and packages source and binaries."
task :dist    => [:dist_zip, :dist_src, :installer, :test]

desc "Builds the documentation and runs the dist task"
task :doc     => [:build_doc, :libdoc, :dist]

desc "Cleans all the object files, binaries, dist packages etc."
task :clean   => [:clean_sln, :clean_sln_old, :clean_doc, :clean_dist]

Albacore.configure do |config|
    config.assemblyinfo do |a|
        a.product_name = PRODUCT_LONG
        a.version      = VERSION
        a.file_version = VERSION
        a.copyright    = COPYRIGHT
        a.company_name = AUTHORS
        a.trademark    = TRADEMARKS
    end
end

desc "Compiles the application."
msbuild :compile  => [:assemblyinfo, :compile_smoz_old] do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Build
    msb.solution = SOLUTION_FILE
    # Values are: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
    msb.verbosity = "detailed"
    msb.log_level = :verbose
    FileUtils.mkdir_p(BUILD_DIR)
    # Disable console logging and send output to a file.
    msb.parameters = "/nologo", "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/msbuild.log\""
end

################################################
# SMOz WinForms targets. Remove when it is obselete
################################################
msbuild :compile_smoz_old  => :assemblyinfo do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Build
    msb.solution = "src/SMOz.WinForms/SMOz.sln"
    # Values are: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
    msb.verbosity = "detailed"
    msb.log_level = :verbose
    FileUtils.mkdir_p(BUILD_DIR)
    # Disable console logging and send output to a file.
    msb.parameters = "/nologo", "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/msbuild.winforms.log\""
end

msbuild :clean_sln_old do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Clean
    msb.solution = "src/SMOz.WinForms/SMOz.sln"
end
###############################################

msbuild :compile_libdoc  => :compile do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => "#{BUILD_DIR}/lib-doc"
    msb.solution = File.expand_path("src/libSmoz/libSmoz.shfbproj")
    msb.verbosity = "detailed"
    msb.log_level = :verbose
    FileUtils.mkdir_p(BUILD_DIR)
    # Disable console logging and send output to a file.
    msb.parameters = "/nologo", "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/shfb.log\"", "/p:DocSourceDir=\"#{OUTPUT_DIR}\""
end

desc "Generates documentation for any class libraries."
task :libdoc => :compile_libdoc  do |t|
    # Copy chm files
    chmfiles = FileList["#{BUILD_DIR}/lib-doc/*.chm"]
    FileUtils.cp_r chmfiles, "#{BIN_DIR}/#{PACKAGE}/"

    # Copy html libdoc files
    #htmlfiles = FileList["#{BUILD_DIR}/lib-doc/*"].exclude(/\.chm$/).exclude(/\/Working/)
    #FileUtils.mkdir_p  "#{BIN_DIR}/#{PACKAGE}/libSMoz.Documentation"
    #FileUtils.cp_r htmlfiles, "#{BIN_DIR}/#{PACKAGE}/libSMoz.Documentation"
end

# Copies the ouput from compile to a proper directory structure
task :build => [:compile]  do
    binaries = FileList["#{OUTPUT_DIR}/*.dll", "#{OUTPUT_DIR}/*.exe", "#{OUTPUT_DIR}/*.exe.config", "#{OUTPUT_DIR}/*.dll.config", "README.md", "COPYING"]

    FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
    FileUtils.cp_r binaries, "#{BIN_DIR}/#{PACKAGE}/"
end

desc "Packages the source code"
task :dist_src do |z|

    gitModules = [
        {"dir" => ".",                "prefix" => "#{PACKAGE}" },
        {"dir" => "lib/Afterthought", "prefix" => "#{PACKAGE}/lib/Afterthought"},
        {"dir" => "doc",              "prefix" => "#{PACKAGE}/doc"}
    ]

    workingdir = Dir.pwd
    FileUtils.rm_rf "#{BUILD_DIR}/src"
    gitModules.each { |m|
        prefix = m["prefix"]
        filename = "#{BUILD_DIR}/src-temp.zip"
        Dir.chdir(m["dir"])
        sh "git archive HEAD --format=zip -9 --prefix=\"#{prefix}/\" > \"#{filename}\""
        Dir.chdir(workingdir)
        extract_zip(filename, "#{BUILD_DIR}/src")
        FileUtils.rm_rf filename
    }

    FileUtils.rm_rf "#{BUILD_DIR}/#{SRC_PACKAGE}.zip"
    zip_dir("#{BUILD_DIR}/src", "#{BUILD_DIR}/#{SRC_PACKAGE}.zip")
end

desc "Ensures that all the git submodules are pulled and at the HEAD of the master branch. You should commit and push all your changes first."
task :update_submodules do
    system("git submodule init")
    system("git submodule update")
    system("git submodule foreach git checkout master")
    system("git submodule foreach git pull")
end

def zip_dir(dir, file)
    path = File.expand_path(dir)
    Zip::ZipFile.open(file, Zip::ZipFile::CREATE) do |zipfile|
        Dir["#{path}/**/**"].each do |file|
            zipfile.add(file.sub(path + '/',''),file)
        end
    end
end

def extract_zip(file, dest)
    Zip::ZipFile.open(file) { |zip_file|
        zip_file.each { |f|
            f_path=File.join(dest, f.name)
            FileUtils.mkdir_p(File.dirname(f_path))
            zip_file.extract(f, f_path) unless File.exist?(f_path)
        }
    }
end

desc "Packages binaries into a distribution ready archive."
zip :dist_zip => [:build] do |z|
    z.directories_to_zip BIN_DIR
    z.output_file = "#{BIN_PACKAGE}.zip"
    z.output_path = BUILD_DIR
end

desc "Runs any unit tests"
mstest :test => [:compile] do |test|
    test.command = "C:/Program Files (x86)/Microsoft Visual Studio 10.0/Common7/IDE/mstest.exe"
    test.assemblies "#{OUTPUT_DIR}/SMOz.Tests.dll"
end

nsisfilelist :installerfiles => [:build] do |n|
    n.dirs << File.expand_path("#{BIN_DIR}/#{PACKAGE}/")
    n.add_files_list = File.expand_path("installer/files_ADD.nsi")
    n.remove_files_list = File.expand_path("installer/files_REM.nsi")
end

desc "Builds the installer"
nsis :installer => [:installerfiles] do |n|
    n.installer_file = File.expand_path("Installer/Installer.nsi")
    n.verbosity = 4
    n.log_file = File.expand_path("#{BUILD_DIR}/installer.log")
    n.defines :PRODUCT_VERSION => VERSION, :OUT_FILE => "#{BUILD_DIR}/#{INS_PACKAGE}.exe"
end

task :assemblyinfo => [:libasminfo, :testsasminfo, :cliasminfo, :winformsasminfo, :wpfasminfo]

assemblyinfo :libasminfo do |a|
    a.title        = "libSmoz"
    a.description  = "A supporting library for automated manipulation of the Windows start menu"
    a.output_file  = "src/libSmoz/Properties/AssemblyInfo.cs"
end

assemblyinfo :testsasminfo do |a|
    a.title        = "SMOz.Tests"
    a.description  = "A set of unit tests for libSMOz features"
    a.output_file  = "src/SMOz.Tests/Properties/AssemblyInfo.cs"
end

assemblyinfo :cliasminfo do |a|
    a.title        = "SMOz.CLI"
    a.description  = "The command-line interface of SMOz"
    a.output_file  = "src/SMOz.CLI/Properties/AssemblyInfo.cs"
end

assemblyinfo :winformsasminfo do |a|
    a.title        = "SMOz.WinForms"
    a.description  = "The (legacy) Windows Forms based graphical user interface of SMOz"
    a.output_file  = "src/SMOz.WinForms/Properties/AssemblyInfo.cs"
end

assemblyinfo :wpfasminfo do |a|
    a.title        = "SMOz.WPF"
    a.description  = "The next generation graphical user interface of SMOz"
    a.output_file  = "src/SMOz.WPF/Properties/AssemblyInfo.cs"
end

msbuild :clean_sln do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Clean
    msb.solution = SOLUTION_FILE
end

task :clean_dist do
    FileUtils.rm_rf BUILD_DIR
end

task :clean_doc do |d|
   FileUtils.rm_rf "doc/.build"
end

desc "Runs Sphinx to build the documentation."
task :build_doc, [:nohtmlhelp, :nolatexpdf, :nohtml] do |d, args|
    # We don't want this littering the dep_graph, call it explicitly!
    Rake::Task["dep_graph"].execute

    targets = []
    targets << "html" if args[:nohtml] == nil
    targets << "latexpdf" if args[:nolatexpdf] == nil
    targets << "htmlhelp" if args[:nohtmlhelp] == nil

    targets = targets.join(" ")

    puts targets.inspect

    currentDir = Dir.pwd()
    Dir.chdir("doc")
      sh "make SPHINXOPTS=\"-D version=#{VERSION} -D release=#{VERSION}\" #{targets}"
      sh "make SPHINXOPTS=\"-D version=#{VERSION} -D release=#{VERSION}\" linkcheck"

      if args[:nohtmlhelp] == nil
        FileUtils.cp_r '.build/htmlhelp/.', 'htmlhelp'
        result = system("hhc htmlhelp/SMOzdoc.hhp")
        FileUtils.cp_r FileList['htmlhelp/*.chm'], '.build/htmlhelp'
        FileUtils.rm_rf "htmlhelp"
     end

    Dir.chdir(currentDir)

    FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
    FileUtils.cp_r FileList['doc/.build/htmlhelp/*.chm'], "#{BIN_DIR}/#{PACKAGE}" if args[:nohtmlhelp] == nil
    FileUtils.cp_r FileList['doc/.build/latex/SMOz.pdf'], "#{BIN_DIR}/#{PACKAGE}" if args[:nolatexpdf] == nil
end

desc "Runs sphinx to build the website."
task :website do |t|

    Rake::Task["build_doc"].invoke(true, true)

    currentDir = Dir.pwd()
    Dir.chdir("website")
      sh "make SPHINXOPTS=\"-D version=#{VERSION} -D release=#{VERSION}\" html"
      sh "make SPHINXOPTS=\"-D version=#{VERSION} -D release=#{VERSION}\" linkcheck"
    Dir.chdir(currentDir)

    FileUtils.mkdir_p "#{BUILD_DIR}/web/"
    FileUtils.cp_r FileList['website/.build/html/**'], "#{BUILD_DIR}/web/"
    FileUtils.mkdir_p "#{BUILD_DIR}/web/doc"
    FileUtils.cp_r FileList['doc/.build/html/**'], "#{BUILD_DIR}/web/doc/"
end

desc "Generates a graph of all the tasks and their relationships."
task :dep_graph do |task|
    this_task = task.name
    dep = RGL::ImplicitGraph.new { |g|
        # vertices of the graph are all defined tasks without this task
        g.vertex_iterator do |b|
            Rake::Task.tasks.each do |t|
                b.call(t) unless t.name == this_task
            end
        end
        # neighbors of task t are its prerequisites
        g.adjacent_iterator { |t, b| t.prerequisites.each(&b) }
        g.directed = true
    }

    dep.write_to_graphic_file('png', this_task)
    puts "Wrote dependency graph to #{this_task}.png."
    dep.write_to_graphic_file('pdf', this_task)
    puts "Wrote dependency graph to #{this_task}.pdf."

    if File.directory? "doc/images"
        FileUtils.cp("#{this_task}.png", "doc/images/#{this_task}.png")
        FileUtils.cp("#{this_task}.pdf", "doc/images/#{this_task}.pdf")
    else
        puts "doc/images directory not found. NOT copying files."
    end
end
