# -*- mode: ruby -*-
# vi: set ft=ruby :

# Vagrantfile API/syntax version. Don't touch unless you know what you're doing!
VAGRANTFILE_API_VERSION = "2"

Vagrant.configure(VAGRANTFILE_API_VERSION) do |config|
  config.vm.box = "centos-6.5-x64"

  config.vm.provider "virtualbox" do |v|
    v.memory = 2048
    v.cpus = 4
  end

  script = <<EOF
yum update -y
yum install -y rpm-build git glib2-devel libpng-devel libjpeg-tubo-devel \
  giflib-devel libtiff-devel libexif-devel libX11-devel fontconfig-devel \
  pcre-devel zlib-devel openssl-devel
yum groupinstall -y 'Development Tools'
EOF

  compile_mono = %{
[[ -d mono ]] || git clone https://github.com/mono/mono.git -b mono-3.12.0-branch
cd mono
[[ -f wrapper.sh ]] || echo <<WRAP >wrapper.sh
#!/bin/sh
export echo=echo
echo $*
exec $*
WRAP
chmod +x wrapper.sh
./autogen.sh --prefix=/usr/local
./wrapper.sh /usr/bin/make get-monolite-latest
./wrapper.sh /usr/bin/make
./wrapper.sh /usr/bin/make install
}
  config.vm.provision :shell, inline: script
  config.vm.provision :shell, inline: compile_mono
end
