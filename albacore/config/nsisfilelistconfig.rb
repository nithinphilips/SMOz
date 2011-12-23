require 'ostruct'
require 'albacore/support/openstruct'

module NsisFileListConfig
    include Albacore::Configuration

    def self.nsisfilelistconfig
        @config ||= OpenStruct.new.extend(OpenStructToHash).extend(NsisFileListConfig)
    end

    def nsisfilelist
        config ||= NsisFileListConfig.nsisfilelistconfig
        yield(config) if block_given?
        config
    end

   def self.included(mod)
      # nothing to do
   end

end


