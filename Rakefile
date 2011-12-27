PRODUCT       = "SMOz"
PRODUCT_LONG  = "SMOz (Start Menu Organizer)"
DESCRIPTION   = "Start Menu Organizer"
VERSION       = "2.0.0" # We omit the build segment of the version number.
AUTHORS       = "Nithin Philips"
COPYRIGHT     = "(c) 2004-2011 #{AUTHORS}"
TRADEMARKS    = "Windows is a trademark of Microsoft Corporation"

CONFIGURATION = "Release"
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
task :doc     => [:build_doc, :dist]

desc "Cleans all the object files, binaries, dist packages etc."
task :clean   => [:clean_sln, :clean_doc, :clean_dist]

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
msbuild :compile  => :assemblyinfo do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Build
    msb.solution = "SMOz.sln"
    # q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
    msb.verbosity = "detailed"

    msb.log_level = :verbose
    FileUtils.mkdir_p(BUILD_DIR)
    # Disable console logging and send output to a file.
    msb.parameters = "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/msbuild.log\""
end

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

task :assemblyinfo => [:libasminfo, :testsasminfo]

assemblyinfo :libasminfo do |a|
    a.title        = "libSmoz"
    a.description  = "A supporting library for automated manipulation of the Windows start menu"
    a.output_file  = "src/libSmoz/Properties/AssemblyInfo.cs"
end

assemblyinfo :testsasminfo do |a|
    a.title        = "SMOz.Tests"
    a.description  = "A set of tests for libSMOz features"
    a.output_file  = "src/SMOz.Tests/Properties/AssemblyInfo.cs"
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
end

msbuild :clean_sln do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Clean
    msb.solution = "SMOz.sln"
end

task :clean_dist do
    FileUtils.rm_rf BUILD_DIR
end

task :clean_doc do |d|
   FileUtils.rm_rf "doc/.build"
end

desc "Runs Sphinx to build the documentation."
task :build_doc do |d|
    currentDir = Dir.pwd()
    Dir.chdir("doc")
      sh 'make html latexpdf htmlhelp'
      sh 'make linkcheck'
      FileUtils.cp_r '.build/htmlhelp/.', 'htmlhelp'

      # @@#&!(# hhc return 1 for OK, 0 for failure. So, ignore it
     result = system("hhc htmlhelp/SMOzdoc.hhp")
     FileUtils.cp_r FileList['htmlhelp/*.chm'], '.build/htmlhelp'
     FileUtils.rm_rf "htmlhelp"

    Dir.chdir(currentDir)

    FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
    FileUtils.cp_r FileList['doc/.build/htmlhelp/*.chm'], "#{BIN_DIR}/#{PACKAGE}"
    FileUtils.cp_r FileList['doc/.build/latex/*.pdf'], "#{BIN_DIR}/#{PACKAGE}"
end
