require 'albacore/albacoretask'
require File.join(File.expand_path(File.dirname(__FILE__)), 'config', 'nsisconfig.rb')

class Nsis
  include Albacore::Task
  include Albacore::RunCommand
  include NsisConfig

  attr_accessor :installer_file, :verbosity, :priority, :log_file, :nocd, :noconfig
  attr_hash :defines

  def initialize
    @nocd = false
    @noconfig = false
    @command = "makensis.exe"
    super()
    update_attributes Albacore.configuration.nsis.to_hash
  end

  def execute
    build_installer(@installer_file)
  end

  def build_installer(installer_file)
    check_installer installer_file

    command_parameters = []
    command_parameters << "/V#{@verbosity}" if @verbosity != nil
    command_parameters << "/P#{@priority}" if @priority != nil
    command_parameters << "/NOCD" if @nocd != false
    command_parameters << "/NOCONFIG" if @noconfig != false
    command_parameters << "\"/O#{@log_file}\"" if @log_file != nil

    command_parameters << build_defines if @defines != nil

    command_parameters << "\"#{installer_file}\""

    @logger.debug command_parameters.join(" ")

    result = run_command "NSIS", command_parameters.join(" ")

    failure_message = 'NSIS Failed. See Installer Log For Detail'
    fail_with_message failure_message if !result
  end

  def check_installer(file)
    return if file
    msg = 'installer script cannot be nil'
    fail_with_message msg
  end

  def build_defines
    option_text = []
    @defines.each do |key, value|
      if value == nil or value.empty?
        option_text << "/D#{key}"
      else
        option_text << "/D#{key}\=\"#{value}\""
      end
    end
    option_text.join(" ")
  end
end

