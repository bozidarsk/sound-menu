# build: mono, gtk-sharp-3
# runtime: mono, gtk-sharp-3, gtk-layer-shell

mcs '-recurse:*.cs' -pkg:gtk-sharp-3.0 -out:sound-menu
chmod 755 ./sound-menu
sudo chown root:root ./sound-menu
sudo mv ./sound-menu /usr/local/bin
