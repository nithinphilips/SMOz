require 'ostruct'
require 'albacore/support/openstruct'

module NsisConfig
    include Albacore::Configuration

    def self.nsisconfig
        @config ||= OpenStruct.new.extend(OpenStructToHash).extend(NsisConfig)
    end

    def nsis
        config ||= NsisConfig.nsisconfig
        yield(config) if block_given?
        config
    end

   def self.included(mod)
      # nothing to do
   end

end



