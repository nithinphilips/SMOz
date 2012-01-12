.. If you want to refer to commits at github, use: ":commit:`c285f31825`."
   If you want to refer to an article on wikipedia, use: ":wikipedia:`article`."

.. toctree::
   :hidden:
   :maxdepth: 1

   Home <self>
   Documentation <http://smoz.sourceforge.net/doc/index.html>
   Reviews <http://sourceforge.net/projects/smoz/reviews/>
   Mailing List <http://sourceforge.net/mail/?group_id=114998>
   Code <http://sourceforge.net/scm/?type=git&group_id=114998>

SMOz
====

.. ifconfig:: releaselevel in ('beta', 'alpha', 'rc')

   .. Note::
      This is a preliminary draft version. It may contain wildly inaccurate information.

.. contents::
   :local:
   :depth: 1

.. figure:: .static/images/start.png
   :align: right
   :height: 250px
   :scale: 100%

   A Start Menu after using SMOz.

SMOz (Start Menu Organizer) helps keep your Windows start menu organized.

SMOz provides an explorer-like view of the start menu where you can
simultaneously organize the start menu shortcuts that are spread
apart in multiple folders. You can quickly and easily drag and drop shortcuts
to categories of your choice. With unlimited undo-redo support, there is no risk.

SMOz also supports storing the categorization you have done to the start menu
as a template file, so you'll never have to repeat the work you've done.

With support for :wikipedia:`regular expression <Regular_expression>` rules in
the template, SMOz allows you to write complex templates. Advanced users can
thus quickly write powerful templates with little effort.

.. container:: download_link

   `Download Links <#download>`_ are below.

Features
--------

* An easy to use interface.
* Drag & Drop support.
* Displays system icons for all items.
* Built-in template editor.
* Unlimited Undo/Redo support.
* Atomically applies changes to the file system.
* Designed to be more easy to expand.

Documentation
-------------
A user manual can be `viewed online`_ and a copy is included with the application.

You can also view the `latest draft copy`_ of the user manual.

.. _viewed online: doc/index.html
.. _latest draft copy: https://github.com/nithinphilips/SMOz/wiki

Getting Help
------------
If you need help send an email to the `mailing list`_ at
<smoz-users@lists.sourceforge.net>. You can `subscribe`_ to it or `browse`_
the archives. 

If you have found a bug, please report it by sending an email to the `mailing list`_.

.. _subscribe:
.. _mailing list: https://lists.sourceforge.net/lists/listinfo/smoz-users
.. _browse: http://sourceforge.net/mailarchive/forum.php?forum_name=smoz-users

Screenshots
-----------

.. container:: screenshots

   .. figure:: .static/images/SMOz.png
      :height: 200px
      :scale: 100%
   
      The main window of SMOz
   
   .. figure:: .static/images/Review.Changes.png
      :height: 200px
      :scale: 100%
   
      Review Changes window before applying a template.
   
   .. figure:: .static/images/Template.Editor.png
      :height: 200px
      :scale: 100%
   
      The template editor.
   
   .. figure:: .static/images/Review.Changes.2.png
      :height: 200px
      :scale: 100%
   
      Review Changes window before applying changes to the start menu.
   
   .. figure:: .static/images/Preferences.png
      :height: 200px
      :scale: 100%
   
      Preferences: A list of files to always ignore.
   
   .. figure:: .static/images/Preferences.2.png
      :height: 200px
      :scale: 100%
   
      Preferences: A list of start menu folders.

What's New
----------

.. include:: releasenote.rst
   :start-after: .. begin block
   :end-before: .. end block

Latest Commits
^^^^^^^^^^^^^^

.. If Javascript is enabled, the following container will be replaced with
   a list of most recent commits. See layout.html for more.

.. container:: github-commits

   You can view a list of the latest commits at `Sourceforge
   <http://smoz.git.sourceforge.net/git/gitweb.cgi?p=smoz/smoz;a=log>`_ or at
   `GitHub <https://github.com/nithinphilips/SMOz/commits>`_ .

Download
--------
The latest version is |release|.

**System Requirements:**
   Any version of Windows supported by `.NET Framework 3.5`_.

   Supports Windows XP, Vista, 7, Server 2003 and Server 2008.

Download: |i-pkg| Installer_, |i-zip| `Zipped Package`_ or |i-src| `Source Code`_.

**Other Options:**

* `See all releases <http://sourceforge.net/projects/smoz/files/smoz/>`_.
* `Browse source code <http://smoz.git.sourceforge.net/>`_.

.. |i-pkg| image:: .static/images/wine.png
.. |i-zip| image:: .static/images/application-x-zip.png
.. |i-src| image:: .static/images/gnome-mime-text-x-csharp.png
.. _.NET Framework 3.5: http://www.microsoft.com/download/en/details.aspx?id=21#system-requirements
