require 'ostruct'
require 'albacore/support/openstruct'

module SphinxConfig
    include Albacore::Configuration

    def self.sphinxconfig
        @config ||= OpenStruct.new.extend(OpenStructToHash).extend(SphinxConfig)
    end

    def sphinx
        config ||= SphinxConfig.sphinxconfig
        yield(config) if block_given?
        config
    end

   def self.included(mod)
      # nothing to do
   end

end



