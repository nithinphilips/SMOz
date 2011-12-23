PRODUCT       = "SMOz"
PRODUCT_LONG  = "SMOz"
DESCRIPTION   = "Start Menu Organizer"
VERSION       = "2.0.0" # We omit the build segment of the version number.
AUTHORS       = "Nithin Philips"
COPYRIGHT     = "(c) 2004-2011 #{AUTHORS}"

CONFIGURATION = "Release"
BUILD_DIR     = "build"
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

task :default => [:dist]
task :dist => [:clean, :dist_zip, :dist_src, :installer]

desc "Compile Application"
msbuild :compile  => :assemblyinfo do |msb|
    msb.properties :configuration => CONFIGURATION, "OutputPath" => OUTPUT_DIR
    msb.targets :Clean, :Build
    msb.solution = "SMOz.sln"
    # q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic]
    msb.verbosity = "detailed"

    msb.log_level = :verbose
    FileUtils.mkdir_p(BUILD_DIR)
    # Disable console logging and send output to a file.
    msb.parameters = "/noconsolelogger", "/fileLogger", "/fileloggerparameters:logfile=\"#{BUILD_DIR}/msbuild.log\""
end

task :build => [:compile]  do
    FileUtils.rm_rf BIN_DIR
    binaries = FileList["#{OUTPUT_DIR}/*.dll", "#{OUTPUT_DIR}/*.exe", "#{OUTPUT_DIR}/*.exe.config", "#{OUTPUT_DIR}/*.dll.config", "README.md", "COPYING"]

    FileUtils.mkdir_p "#{BIN_DIR}/#{PACKAGE}/"
    FileUtils.cp_r binaries, "#{BIN_DIR}/#{PACKAGE}/"
end

desc "Package source code"
task :dist_src do |z|
    FileUtils.rm_rf "#{BUILD_DIR}/src"
    FileUtils.mkdir_p "#{BUILD_DIR}/src"
    sh "svn export . \"#{BUILD_DIR}/src/#{SRC_PACKAGE}\""
    zip_dir("#{BUILD_DIR}/src", "#{BUILD_DIR}/#{SRC_PACKAGE}.zip")
end

def zip_dir(dir, file)
    path = File.expand_path(dir)
    Zip::ZipFile.open(file, Zip::ZipFile::CREATE) do |zipfile|
        Dir["#{path}/**/**"].each do |file|
            zipfile.add(file.sub(path + '/',''),file)
        end
    end
end

desc "Package binaries"
zip :dist_zip => [:build] do |z|
    z.directories_to_zip BIN_DIR
    z.output_file = "#{BIN_PACKAGE}.zip"
    z.output_path = BUILD_DIR
end

desc "Update installer file list"
nsisfilelist :installerfiles => [:build] do |n|
    n.dirs << File.expand_path("#{BIN_DIR}/#{PACKAGE}/")
    n.add_files_list = File.expand_path("installer/files_ADD.nsi")
    n.remove_files_list = File.expand_path("installer/files_REM.nsi")
end

desc "Build installer"
nsis :installer => [:installerfiles] do |n|
    n.installer_file = File.expand_path("Installer/Installer.nsi")
    n.verbosity = 4
    n.log_file = File.expand_path("#{BUILD_DIR}/installer.log")
    n.defines :PRODUCT_VERSION => VERSION, :OUT_FILE => File.expand_path("#{BUILD_DIR}/#{INS_PACKAGE}.exe")
end

desc "Cleanup files"
task :clean do
    FileUtils.rm_rf BUILD_DIR
end

assemblyinfo :assemblyinfo do |a|
    a.product_name = PRODUCT_LONG
    a.version      = VERSION
    a.file_version = VERSION
    a.copyright    = COPYRIGHT
    a.company_name = AUTHORS
    a.title        = "LibSMOz"
    a.description  = DESCRIPTION
    a.output_file  = "src/libSmoz/Properties/AssemblyInfo.cs"
end

desc "Generate a graph of all the tasks and their relationship"
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
