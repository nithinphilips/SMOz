
**********
Appendices
**********

.. index:: GPL license
.. index:: FDL license

License
=======

This document is published under the GNU FDL::

    Copyright (C) 2011 Keith Brooke, Nithin Philips

    Permission is granted to copy, distribute and/or modify this document
    under the terms of the GNU Free Documentation License, Version 1.3
    or any later version published by the Free Software Foundation;
    with no Invariant Sections, no Front-Cover Texts, and no Back-Cover Texts.

A copy of the license is available at <http://www.gnu.org/licenses/fdl.html>.

SMOz is free software::

    Copyright (C) 2011 Nithin Philips <nithin@nithinphilips.com>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

Credits
=======

.. todo:: Write Credits

.. index:: Help, Mailing list, SourceForge support website
.. index:: see: Support; Help

Getting Help
============
Check the SourceForge website to see the best way to receive support:
https://sourceforge.net/projects/smoz/support

A mailing list is available at <smoz-users@lists.sourceforge.net> . You
can `subscribe <https://lists.sourceforge.net/lists/listinfo/smoz-users>`_ to it or
`browse <https://sourceforge.net/mailarchive/forum.php?forum_name=smoz-users>`_
archives.

Contributing
============
It's easy to contribute to SMOz.

If you are a user, help is needed in providing support for other users,
maintaining the website and the user manual, and more. If you'd like to help,
please get in touch: <nithin@nithinphilips.com>

If you are a programmer, you can `fork <http://help.github.com/fork-a-repo/>`_
the repository at GitHub, make changes to SMOz, and when you're ready, send me a `pull request
<http://help.github.com/send-pull-requests/>`_ to have your changes integrated
into the primary repo. You can also send your changes as standard patch files.

Building SMOz
=============
You can use either Visual Studio or `Rake
<http://rake.rubyforge.org/>`_  with `Albacore <http://albacorebuild.net/>`_ to
build the project. Building with Rake is the preferred method and it
is described here.

.. index:: Ruby, .NET 4.0 SDK, Rake, Albacore, RubyZip, Rgl, Git
.. index:: see: Windows 7 SDK; .NET 4.0 SDK

Setting Up the Build Environment
--------------------------------
1. Install `Git for Windows <https://code.google.com/p/msysgit/>`_
2. Install the `Windows 7 and .NET 4.0 SDK
   <http://msdn.microsoft.com/en-us/windows/bb980924.aspx>`_.
3. Install Ruby using the `RubyInstaller <http://rubyinstaller.org/>`_.
4. After installing ruby, from the Command Prompt, run::

    gem install rake
    gem install albacore
    gem install rgl
    gem install rubyzip

5. Make sure that the paths to the tools, git, .NET framework tools
   and ruby are in your ``PATH``.

For more information on Albacore, visit the `Albacore wiki
<https://github.com/derickbailey/Albacore/wiki/>`_.

.. index:: Python, Sphinx, HTML Help Workshop, TeX Live, Github, Cygwin

Getting the Source and Building
-------------------------------
You can checkout the latest source code via git. Two public mirrors are
available, at `SourceForge
<http://smoz.git.sourceforge.net/git/gitweb.cgi?p=smoz/smoz>`_
and `GitHub <https://github.com/nithinphilips/SMOz>`_.

From SourceForge::

    git clone git://smoz.git.sourceforge.net/gitroot/smoz/smoz

Or, from GitHub::

    git clone git://github.com/nithinphilips/SMOz.git

To build SMOz, open the Command prompt at the project root and run::

    rake

The default target compiles the code, builds the installer and creates all the
packages necessary for distribution. There are a few different targets
available for other tasks:

================== ============================================================
  Target                            Description
================== ============================================================
build_doc           Runs Sphinx to build the documentation.
clean               Cleans all the object files, binaries, dist packages etc.
compile             Compiles the application.
default             Runs the dist task
dep_graph           Generates a graph of all the tasks and their relationships.
dist                Builds the application, installer and packages source and
                    binaries.
dist_src            Packages the source code
dist_zip            Packages binaries into a distribution ready archive.
doc                 Builds the documentation and runs the dist task
installer           Builds the installer
test                Runs any unit tests
update_submodules   Ensures that all the git submodules are pulled and at the
                    HEAD of the master branch.
================== ============================================================

To run tasks, add the task name after the rake command. For example:  ``rake
dist``.

.. index:: HTML documentation, CHM documentation, PDF documentation

.. index:: Dependency graph

SMOz Rakefile Dependency
------------------------

.. image:: images/dep_graph.*
   :alt: Detailed dependency graph of SMOz Rakefile

|pagebreak|

Building the SMOz Documentation
===============================

Setting up the Documentation Build Environment
-----------------------------------------------
The documentation for SMOz is written using `Sphinx
<http://sphinx.pocoo.org/>`_. You can view the latest documentation online at
https://github.com/nithinphilips/SMOz/wiki

If you'd like to generate HTML, PDF or HTML Help formats of the documentation,
you'll need to install Sphinx and the required tools.

1. Install  `Sphinx <http://sphinx.pocoo.org/>`_

   Before installing Sphinx, you'll need to install `Python
   <http://www.python.org/>`_ and `setuptools
   <http://pypi.python.org/pypi/setuptools>`_.

   There are two ways to install Python. If you already have `Cygwin
   <http://www.cygwin.com/>`_ installed, you can use the Python package from
   Cygwin.  Otherwise, see http://www.python.org/download/ for the Windows
   installer.

   Depending on how you installed Python, follow the proper instructions to
   install setuptools.

   * Python Windows Installer: see
     http://pypi.python.org/pypi/setuptools#windows
   * Cygwin package: see
     http://pypi.python.org/pypi/setuptools#cygwin-mac-os-x-linux-other

   Once setuptools are installed, run::

       easy_install -U Sphinx

2. Install `HTML Help Workshop
   <https://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21138>`_

   `HTML Help Workshop
   <https://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21138>`_
   is required to create ``.chm`` output.

   Download and install it from
   `https://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21138`.

   You should add the install path your ``PATH`` environment variable.

3. Install `Tex Live <http://www.tug.org/texlive/>`_

   `Tex Live <http://www.tug.org/texlive/>`_ is required to create ``.pdf``
   output.

   Download and install it from http://www.tug.org/texlive/acquire-netinstall.html

   .. NOTE::
      If you have Cygwin installed, run the installation script from a Cygwin
      shell to install it there.

   You should add the install path your ``PATH`` environment variable.

.. index:: Building SMOz, Running Rake, Build targets, Build dependency graph

Getting the Source and Building
-------------------------------
You can checkout the latest source code via git. Two public mirrors are
available, at `SourceForge
<http://smoz.git.sourceforge.net/git/gitweb.cgi?p=smoz/smoz>`_
and `GitHub <https://github.com/nithinphilips/SMOz>`_.

From SourceForge::

    git clone git://smoz.git.sourceforge.net/gitroot/smoz/smoz

Or, from GitHub::

    git clone git://github.com/nithinphilips/SMOz.git

To build SMOz, open the Command prompt at the project root and run::

    rake

The default task compiles the code, builds the installer and creates all the
packages necessary for distribution.

The default task does not build the documentation. To include the documentation
in your distribution, run::

    rake doc dist

To see a list of all available tasks, run::

    rake -T

These are the currently available tasks:

================== ============================================================
  Target                            Description
================== ============================================================
build              Compiles the application
clean              Cleans all the object files, binaries, dist packages etc.
dep_graph          Generates a graph of all the tasks and their relationships.
deploy:packages    Packages the application and uploads it to the SourceForge
                   website.
deploy:website     Builds and uploads the website to the SourceForge server.
dist               Builds the application, installer and packages source and
                   binaries (the default).
dist:bin           Packages binaries into a distribution ready archive.
dist:installer     Packages the binaries into a Windows installer.
dist:src           Packages the source code into an archive.
doc                Builds the documentation.
doc:dev            Builds developer's documentation for any class libraries.
doc:usr            Builds the application user manual using Sphinx.
doc:website        Builds the website using Sphinx.
tests              Runs any unit tests.
================== ============================================================

.. index:: Dependency graph

Rake tasks are often dependent on other tasks to perform parts of their job.
The following graph has a complete list all tasks in the SMOz Rakefile and
their relationship to each other. It may be helpful when trying to achieve
something that is not already supported.

.. image:: images/dep_graph.*
   :alt: Detailed dependency graph of SMOz Rakefile
