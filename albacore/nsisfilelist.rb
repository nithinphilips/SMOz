# NSIS requires all paths to use the Windows \ separator, so we
# use File::ALT_SEPARATOR. So, obviously this task will not
# work on a UNIX box.

require 'albacore/albacoretask'

class NsisFileList
    include Albacore::Task
    include Albacore::RunCommand
    include NsisFileListConfig

    attr_accessor :add_files_list, :remove_files_list

    attr_array :dirs           # Directories containing source files.

    alias :dir :dirs
    alias :dir= :dirs=

    alias :rem_files_list :remove_files_list
    alias :rem_files_list= :remove_files_list=

    def initialize
        @dirs = []
        super()
        update_attributes Albacore.configuration.nsisfilelist.to_hash
    end

    def execute
        cwd = Dir.pwd.gsub(File::SEPARATOR, File::ALT_SEPARATOR) # Use platform specific path separator

        FileUtils.mkdir_p(File.dirname(@add_files_list))
        FileUtils.mkdir_p(File.dirname(@remove_files_list))

        @logger.info "Generating Add Files list file At: " + File.expand_path(@add_files_list)
        @logger.info "Generating Remove Files list file At: " + File.expand_path(@remove_files_list)

        @dirs = [@dirs] if @dirs.is_a? String

        File.open(@add_files_list, 'w') do |add_file|
            File.open(@remove_files_list, 'w') do |rem_file|
                for dir in @dirs
                    dir.gsub!(File::SEPARATOR, File::ALT_SEPARATOR)
                    RecurseTree(dir, add_file, rem_file, dir, cwd)
                end
            end
        end
    end

    def RecurseTree(directory, add_file, rem_file, src_root, cwd)

        fail_with_message "At least one directory must be specified as dirs" if !directory
        fail_with_message "An add_files_list must be specified" if !add_file
        fail_with_message "A remove_files_list must be specified" if !rem_file

        out_path = directory.sub(src_root, "$INSTDIR")

        add_file.puts  ""
        add_file.puts  "SetOutPath \"#{out_path}\""
        add_file.puts  "SetOverwrite ifnewer"

        dir = Dir.open(directory)

        for path in dir
            path = directory + File::ALT_SEPARATOR + path;
            unless FileTest.directory?(path)
                add_line = path.sub(cwd, "..")
                add_file.puts "File \"#{add_line}\""

                rem_line = path.sub(src_root, "$INSTDIR")
                rem_file.puts "Delete \"#{rem_line}\""
            end
        end

        for path in dir
            next if path == "." or path == ".."

            path = directory + File::ALT_SEPARATOR + path;

            if File.lstat(path).directory? then
                RecurseTree(path, add_file, rem_file, src_root, cwd)
            end
        end


        rem_file.puts "RmDir \"#{out_path}\""
        rem_file.puts ""
    end

    def check_file(file)
        return if file
        msg = 'output file cannot be nil'
        fail_with_message msg
    end

end

