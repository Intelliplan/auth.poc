require 'bundler/setup'

require 'albacore'
require 'albacore/tasks/versionizer'
require 'albacore/ext/teamcity'

Albacore::Tasks::Versionizer.new :versioning

desc 'Perform fast compilation (warn: doesn\'t d/l deps)'
build :quick_compile do |b|
  b.logging = 'detailed'
  b.sln     = 'app/auth.poc.sln'
end

desc 'restore all nugets as per the packages.config files'
nugets_restore :restore do |p|
  p.out = 'packages'
  p.exe = 'tools/NuGet.exe'
end

desc 'Perform full compilation'
build :compile => [:versioning, :restore] do |b|
  b.sln = 'app/auth.poc.sln'
end

task :default => :compile
