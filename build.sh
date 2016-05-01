#!/bin/bash -e

SOLUTION=FsOptics.sln

if hash mono &> /dev/null ; then
  RUNNER=mono
else
  RUNNER=
fi

if [ ! -f .paket/paket.exe ] ; then
  $RUNNER .paket/paket.bootstrapper.exe
fi

PAKET=.paket/paket.exe

$RUNNER $PAKET install

if hash xbuild &> /dev/null ; then
  BUILD=xbuild
elif hash msbuild.exe &> /dev/null ; then
  BUILD=msbuild.exe
else
  echo "Couldn't find build command."
  exit 1
fi

function build() {
  $BUILD /nologo /verbosity:quiet /p:Configuration=$2 $1
}

if [ "$1" != "" ] ; then
  build $SOLUTION "$*"
else
  build $SOLUTION Debug
  build $SOLUTION Release

  $RUNNER $PAKET pack output . templatefile FsOptics.paket.template
fi
