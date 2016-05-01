#!/bin/bash -e

./build.sh Debug

for fsi in fsharpi fsianycpu.exe fsi.exe ; do
    if hash $fsi &> /dev/null ; then
        rlwrap -t dumb $fsi --use:FsOptics.fsx
        exit
    fi
done
