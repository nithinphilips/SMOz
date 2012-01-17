PRODUCT       = "SMOz"
PRODUCT_LONG  = "SMOz (Start Menu Organizer)"
DESCRIPTION   = "Start Menu Organizer"
VERSION       = ENV['VERSION'] || "1.1.0"
AUTHORS       = "Nithin Philips"
COPYRIGHT     = "(c) 2004-2012 #{AUTHORS}"
TRADEMARKS    = "Windows is a trademark of Microsoft Corporation"

CONFIGURATION = ENV['CONFIGURATION'] || "Release" # The configuration to build: Release or Debug.
RELEASE_LEVEL = ENV['REL'] || "beta"
SOLUTION_FILE = "SMOz.sln"               # Name of the main visual studio solution/project file

BUILD_DIR     = File.expand_path("build")
OUTPUT_DIR    = "#{BUILD_DIR}/out"      # Where the output from msbuild is placed.
BIN_DIR       = "#{BUILD_DIR}/bin"      # Where the input files for dist:bin and dist:installer are placed.
SRC_DIR       = "#{BUILD_DIR}/src"      # Temp dir where the source for dist_src is placed.
PKG_DIR       = "#{BUILD_DIR}/packages" # Where the packages will go.
WEB_DIR       = "build/web"             # Where the built website is placed.
DOC_DIR       = "build/doc"

PACKAGE       = "#{PRODUCT}-#{VERSION}" 
BIN_PACKAGE   = "#{PACKAGE}-bin"        # Name of the archive with all the binaries/executables.
SRC_PACKAGE   = "#{PACKAGE}-src"        # Name of the archive with the source code.
INS_PACKAGE   = "#{PACKAGE}-setup"      # Name of the installer.

README_FILE   = "releasenotes/Release-#{VERSION}.rst"

require 'albacore'
FileList["./albacore/*.rb"].each { |f| require f }
require 'rgl/dot'
require 'rgl/implicit'
require 'zip/zip'
require 'zip/zipfilesystem'
require 'net/scp'

task :default => [:dist]

desc "Builds the application, installer and packages source and binaries (the default)."
task :dist    => ["dist:bin", "dist:src", "dist:installer", :tests]

desc "Builds all documentation."
task :doc     => ["doc:usr", "doc:dev"]

desc "Cleans all the object files, binaries, dist packages etc."
task :clean   => ["clean:sln", "clean:sln_winforms", "clean:doc", "clean:website", "clean:dist"]

task :website => "doc:website"

Albacore.configure do |config|
    config.assemblyinfo do |a|
        a.product_name = PRODUCT_LONG
        a.version      = VERSION
        a.file_version = VERSION
        a.copyright    = COPYRIGHT
        a.company_name = AUTHORS
        a.trademark    = TRADEMARKS
    end

    config.sphinx do |s|
        s.sphinxbuild_script  = "/usr/bin/sphinx-build"
        s.sourcedir = "doc"
        s.builddir  = DOC_DIR
        s.define = { "version" => VERSION, "release" => VERSION, "releaselevel" => RELEASE_LEVEL }
    end
end

# Compiles the application.
msbuild :compile  => [:assemblyinfo] do |msb|
    puts "Compiling #{SOLUTION_FILE}"
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

# Compiles the (legacy) windows forms version of the application.
msbuild :compile_smoz_winforms  => :assemblyinfo do |msb|
    puts "Compiling the legacy windows forms application"
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

desc "Compiles the application" # and copies the ouput from compile to a proper directory structure
task :build => [:compile, :compile_smoz_winforms]  do
    puts "Copying selected output to final destination"
    binaries = FileList["#{OUTPUT_DIR}/*.dll", 
                        "#{OUTPUT_DIR}/*.exe", 
                        "#{OUTPUT_DIR}/*.exe.config", 
                        "#{OUTPUT_DIR}/*.dll.config", 
                        "README.md", 
                        "COPYING",
                        "res/Template.ini"]

    FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
    FileUtils.cp_r binaries, "#{BIN_DIR}/#{PACKAGE}/"
end

# Ensures that all the git submodules are pulled and at the HEAD of the master
# branch. You should commit and push all your changes first.
task :update_submodules do
    puts "Forwarding submodules to the tip of their master branch"
    system("git submodule init")
    system("git submodule update")
    system("git submodule foreach git checkout master")
    system("git submodule foreach git pull")
end

namespace :dist do
    desc "Packages the source code into an archive."
    task :src do |z|
        puts "Packaging the source code"

        gitModules = [
            {"dir" => ".",                "prefix" => "#{PACKAGE}" },
            {"dir" => "lib/Afterthought", "prefix" => "#{PACKAGE}/lib/Afterthought"},
            {"dir" => "doc",              "prefix" => "#{PACKAGE}/doc"}
        ]

        workingdir = Dir.pwd
        FileUtils.rm_rf "#{SRC_DIR}"

        gitModules.each { |m|
            prefix = m["prefix"]
            filename = "#{BUILD_DIR}/src-temp.zip"
            Dir.chdir(m["dir"])
            sh "git archive HEAD --format=zip -9 --prefix=\"#{prefix}/\" > \"#{filename}\""
            Dir.chdir(workingdir)
            extract_zip(filename, "#{SRC_DIR}")
            FileUtils.rm_rf filename
        }

        FileUtils.mkdir_p PKG_DIR
        FileUtils.rm_rf "#{PKG_DIR}/#{SRC_PACKAGE}.zip"
        zip_dir("#{SRC_DIR}", "#{PKG_DIR}/#{SRC_PACKAGE}.zip")

        FileUtils.rm_rf "#{SRC_DIR}"
    end

    desc "Packages binaries into a distribution ready archive."
    zip :bin => [:build] do |z|
        FileUtils.mkdir_p PKG_DIR

        z.directories_to_zip BIN_DIR
        z.output_file = "#{BIN_PACKAGE}.zip"
        z.output_path = PKG_DIR
    end

    nsisfilelist :installerfiles => [:build] do |n|
        n.dirs << File.expand_path("#{BIN_DIR}/#{PACKAGE}/")
        n.add_files_list = File.expand_path("installer/files_ADD.nsi")
        n.remove_files_list = File.expand_path("installer/files_REM.nsi")
    end

    desc "Packages the binaries into a Windows installer."
    nsis :installer => ["dist:installerfiles"] do |n|
        puts "Building the installer"
        n.installer_file = File.expand_path("Installer/Installer.nsi")
        n.verbosity = 4
        n.log_file = File.expand_path("#{BUILD_DIR}/installer.log")
        n.defines :PRODUCT_VERSION => VERSION, :OUT_FILE => "#{PKG_DIR}/#{INS_PACKAGE}.exe"
    end
end

desc "Runs any unit tests."
mstest :tests => [:compile] do |test|
    test.command = "C:/Program Files (x86)/Microsoft Visual Studio 10.0/Common7/IDE/mstest.exe"
    test.assemblies "#{OUTPUT_DIR}/SMOz.Tests.dll"
end

task :assemblyinfo => ["asminfo:libsmoz", "asminfo:tests",
                       "asminfo:cli", "asminfo:winforms", "asminfo:wpf"]

namespace :asminfo do
    assemblyinfo :libsmoz do |a|
        a.title        = "libSmoz"
        a.description  = "A supporting library for automated manipulation of the Windows start menu"
        a.output_file  = "src/libSmoz/Properties/AssemblyInfo.cs"
    end

    assemblyinfo :tests do |a|
        a.title        = "SMOz.Tests"
        a.description  = "A set of unit tests for libSMOz features"
        a.output_file  = "src/SMOz.Tests/Properties/AssemblyInfo.cs"
    end

    assemblyinfo :cli do |a|
        a.title        = "SMOz.CLI"
        a.description  = "The command-line interface of SMOz"
        a.output_file  = "src/SMOz.CLI/Properties/AssemblyInfo.cs"
    end

    assemblyinfo :winforms do |a|
        a.title        = "SMOz.WinForms"
        a.description  = "The (legacy) Windows Forms based graphical user interface of SMOz"
        a.output_file  = "src/SMOz.WinForms/Properties/AssemblyInfo.cs"
    end

    assemblyinfo :wpf do |a|
        a.title        = "SMOz.WPF"
        a.description  = "The next generation graphical user interface of SMOz"
        a.output_file  = "src/SMOz.WPF/Properties/AssemblyInfo.cs"
    end
end

namespace :clean do
    msbuild :sln do |msb|
        msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
        msb.targets :Clean
        msb.solution = SOLUTION_FILE
    end

    task :dist do
        FileUtils.rm_rf BUILD_DIR
    end

    task :doc do |d|
        FileUtils.rm_rf DOC_DIR
    end

    task :website do |d|
        FileUtils.rm_rf WEB_DIR
    end

    msbuild :sln_winforms do |msb|
        msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
        msb.targets :Clean
        msb.solution = "src/SMOz.WinForms/SMOz.sln"
    end
end

namespace :doc do
    desc "Builds the application user manual using Sphinx."
    task :usr => ["doc:usr_html", "doc:usr_htmlhelp", "doc:usr_latexpdf", "doc:usr_linkcheck"] do

        FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
        FileUtils.cp_r FileList["#{DOC_DIR}/htmlhelp/*.chm"], "#{BIN_DIR}/#{PACKAGE}"
        FileUtils.cp_r FileList["#{DOC_DIR}/latex/SMOz.pdf"], "#{BIN_DIR}/#{PACKAGE}"
    end

    sphinx :usr_html do |s|
        Rake::Task["dep_graph"].invoke unless File.exist?("doc/images/dep_graph.png");
        s.builder = :html
        puts "Building documentation in #{s.builder.to_s} from #{s.sourcedir} into #{s.builddir}."
    end

    sphinx :usr_htmlhelp do |s|
        Rake::Task["dep_graph"].invoke unless File.exist?("doc/images/dep_graph.png");
        s.builder = :htmlhelp
        puts "Building documentation in #{s.builder.to_s} from #{s.sourcedir} into #{s.builddir}."
    end

    sphinx :usr_latexpdf do |s|
        Rake::Task["dep_graph"].invoke unless File.exist?("doc/images/dep_graph.png");
        s.builder = :latexpdf
        puts "Building documentation in #{s.builder.to_s} from #{s.sourcedir} into #{s.builddir}."
    end

    sphinx :usr_linkcheck do |s|
        Rake::Task["dep_graph"].invoke unless File.exist?("doc/images/dep_graph.png");
        s.builder = :linkcheck
        puts "Checking links in documentation built from #{s.sourcedir} into #{s.builddir}."
    end

    msbuild :compile_dev  => :compile do |msb|
        puts "Compiling library documentation"
        msb.properties :configuration => CONFIGURATION, "OutputPath" => "#{BUILD_DIR}/lib-doc"
        msb.solution = File.expand_path("src/libSmoz/libSmoz.shfbproj")
        msb.verbosity = "detailed"
        msb.log_level = :verbose
        FileUtils.mkdir_p(BUILD_DIR)
        # Disable console logging and send output to a file.
        msb.parameters = "/nologo", "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/shfb.log\"", "/p:DocSourceDir=\"#{OUTPUT_DIR}\""
    end

    desc "Builds developer's documentation for any class libraries."
    task :dev => "doc:compile_dev"  do |t|
        # Copy chm files
        chmfiles = FileList["#{BUILD_DIR}/lib-doc/*.chm"]
        FileUtils.cp_r chmfiles, "#{BIN_DIR}/#{PACKAGE}/"
    end

    desc "Builds the website using Sphinx."
    task :website => ["doc:usr_html", "doc:website_html"] do |t|
        FileUtils.mkdir_p "#{WEB_DIR}/html/doc"
        FileUtils.cp_r FileList["#{DOC_DIR}/html/**"], "#{WEB_DIR}/html/doc/"
    end

    sphinx :website_html do |s|

        # These links in the website reST are changes based on version.
        # Provide them via the rst_epilog file.

        File.open("website/rst_epilog.txt", 'w') do |f|

            f.puts File.open("website/rst_epilog.txt.in", 'r').read if File.exist?("website/rst_epilog.txt.in")

            f.puts ".. _Installer: http://sourceforge.net/projects/smoz/files/smoz/#{VERSION}/#{INS_PACKAGE}.exe/download"
            f.puts ".. _Zipped Package: http://sourceforge.net/projects/smoz/files/smoz/#{VERSION}/#{BIN_PACKAGE}.zip/download"
            f.puts ".. _Source Code: http://sourceforge.net/projects/smoz/files/smoz/#{VERSION}/#{SRC_PACKAGE}.zip/download"

            f.puts ".. |releasedate| replace:: " + DateTime.now.strftime("%a %b %-d, %Y")
        end

        unless File.exist?(README_FILE) 
            fail "A release note is required to build the website.\nWrite one named '#{README_FILE}' and place it in the 'releasenotes' subdirectory and run this task again."
        end

        FileUtils.cp_r README_FILE, "website/releasenote.rst"

        s.sourcedir = "website"
        s.builddir = WEB_DIR
        s.builder = :html
        puts "Building website in #{s.builder.to_s} from #{s.sourcedir} into #{s.builddir}."
    end

    sphinx :website_linkcheck do |s|
        s.sourcedir = "website"
        s.builddir = WEB_DIR
        s.builder = :linkcheck
        puts "Checking links in website built from #{s.sourcedir} into #{s.builddir}."
    end
end

desc "Deploys the packages, then the website"
task :deploy => ["deploy:packages", "deploy:website"]

namespace :deploy do

    SourceforgeUsername   = "spikiermonkey,smoz"
    WebsiteRemoteHost = "web.sourceforge.net"
    WebsiteRemoteDir  = "/home/project-web/smoz/htdocs/"
    FilesRemoteHost = "frs.sourceforge.net"
    FilesRemoteDir  = "/home/pfs/project/s/sm/smoz/smoz/#{VERSION}/"  # the trailing slash is important

    desc "Packages the application and uploads it to the SourceForge website."
    task :packages => [:clean, :doc, :dist] do |t|

        unless File.exist?(README_FILE) 
            fail "A release note is required to deploy this package.\nWrite one named '#{README_FILE}' and place it in the 'releasenotes' subdirectory and run this task again."
        end
        FileUtils.cp(README_FILE, "#{PKG_DIR}/README.rst")

        files =  FileList["build/packages"]
        files.each { |f| sh "scp -r \"#{f}\" #{SourceforgeUsername}@#{FilesRemoteHost}:#{FilesRemoteDir}" }
    end

    desc "Builds and uploads the website to the SourceForge server."
    task :website => ['doc:website', "doc:usr_linkcheck", "doc:website_linkcheck"] do |t|
        puts "Deploying website to #{SourceforgeUsername}@#{WebsiteRemoteHost}:#{WebsiteRemoteDir}"
        files =  FileList["#{WEB_DIR}/html/**"]
        files.each { |f| sh "scp -r \"#{f}\" #{SourceforgeUsername}@#{WebsiteRemoteHost}:#{WebsiteRemoteDir}" }
    end
end

namespace :stage do
    task :website do
            WebsiteRemoteDir = "/home/project-web/smoz/htdocs/staging/"
            Rake::Task["deploy:website"].invoke
    end
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
