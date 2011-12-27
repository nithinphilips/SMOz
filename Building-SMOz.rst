Building SMOz
=============
You can use either Visual Studio or `Rake <http://rake.rubyforge.org/>`_ with
`Albacore <http://albacorebuild.net/>`_ to build the project. Building with Rake
is the preferred method and it is described below.

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

5. Make sure that the paths to the tools, git, .NET framework tools and ruby
   are in your ``PATH``.

For more information on Albacore, visit the `Albacore wiki
<https://github.com/derickbailey/Albacore/wiki/>`_.


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

   Download and install it from `https://www.microsoft.com/download/en/details.aspx?displaylang=en&id=21138`.

   You should add the install path your ``PATH`` environment variable.

3. Install `Tex Live <http://www.tug.org/texlive/>`_

   `Tex Live <http://www.tug.org/texlive/>`_ is required to create ``.pdf`` output.

   Download and install it from http://www.tug.org/texlive/acquire-netinstall.html

   .. NOTE::
      If you have Cygwin installed, run the installation script from a Cygwin shell
      to install it there.

   You should add the install path your ``PATH`` environment variable.

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

=============== =================================================================
  Target                            Description
=============== =================================================================
 dist            The default. Compiles code, builds installer, packages binaries
                 and source code for distribution.
 dist_with_log   Same as dist, except logging will be enabled.
 installer       Compiles code and builds the installer.
 build           Compiles the code.
 dist_zip        Compiles the code and packages the binaries into a zip file.
 dist_src        Packages the source code into a zip file.
 clean           Deletes all the output created by all other tasks.
 dep_graph       Creates a nice graph with all available targets and their
                 relationship.
=============== =================================================================

To run tasks, add the task name after the rake command. For example:  ``rake
dist``.

.. figure:: images/dep_graph.png
          :alt: The SMOz build script dependency graph.
          :align: center

          The SMOz build script dependency graph.

Getting the Documentation Source and Building
---------------------------------------------
The Documentation is hosted at GitHub.

To checkout the source, run::

    git clone git://github.com/nithinphilips/SMOz.wiki.git

The documentation uses a ``makefile`` to build. To build the ``html`` output format,
run::

    make html

The following targets are available:

=========== ================================================================
  Target                            Description
=========== ================================================================
 html        to make standalone HTML files
 dirhtml     to make HTML files named index.html in directories
 singlehtml  to make a single large HTML file
 pickle      to make pickle files
 json        to make JSON files
 htmlhelp    to make HTML files and a HTML help project
 qthelp      to make HTML files and a qthelp project
 devhelp     to make HTML files and a Devhelp project
 epub        to make an epub
 latex       to make LaTeX files, you can set PAPER=a4 or PAPER=letter
 latexpdf    to make LaTeX files and run them through pdflatex
 text        to make text files
 man         to make manual pages
 texinfo     to make Texinfo files
 info        to make Texinfo files and run them through makeinfo
 linkcheck   to check all external links for integrity
=========== ================================================================
