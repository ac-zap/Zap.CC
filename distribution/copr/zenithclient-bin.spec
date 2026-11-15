%global debug_package %{nil}
%global __strip /bin/true
%global _appname ZenithClient
%global _rpmfile %{_appname}_%{version}_linux_x86_64.rpm

Name:           zenithclient-bin
Version:        2.10.1
Release:        1%{?dist}
Summary:        Next-generation open source Minecraft launcher (prebuilt)

License:        GPL-3.0-only
URL:            https://github.com/ac-zap/Zap.CC
Source0:        %{url}/releases/download/zenithclient-%{version}/%{_rpmfile}

ExclusiveArch:  x86_64
BuildRequires:  bsdtar
Requires:       libxdo
Requires:       dbus-libs
Provides:       ZenithClient = %{version}-%{release}

%description
ZenithClient is a next-generation open source Minecraft launcher.
This package ships the prebuilt binaries from the upstream release.

%prep
%setup -q -c -T
# Repackages the upstream release rpm; bsdtar reads its cpio payload directly.
bsdtar -xf %{SOURCE0}

%install
mkdir -p %{buildroot}
cp -a usr %{buildroot}/
ln -s ZenithClient_app %{buildroot}%{_bindir}/ZenithClient

%files
%{_bindir}/ZenithClient_app
%{_bindir}/ZenithClient
%{_datadir}/applications/*.desktop
%{_datadir}/icons/hicolor/*/apps/*

%changelog
* Sun Nov 15 2026 ZenithClient - 2.10.1-1
- Rebranded to Zenith Client.
