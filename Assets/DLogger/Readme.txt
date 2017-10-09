DLogger 
=======
A logger for Unity that provides a quicker external log file that it more controllable
by the developer. A html version of the log can be generated and can be decorated using
stylesheets.

How To Use DLogger
==================

Using the DLogger is simple. Where you would usually use

	Debug.Log("Some Debug Information");
	
use this instead

	D.log("Some Debug Information");
	
Its that simple. Out of the box, all logging events will go to the Unity console. If this is not
desierable, simply remove the compiling directive DEBUG_TOCONSOLE from the D.css class, or change
it in your global defines (more information below).

How to View the Logs
====================

When the log files are generated they will default to the location specified in the
Application.dataPath folder with a filename of "dlogger".

The TEXT version (dlogger.txt) can be viewed by any text reader.

The HTML version (dlogger.html) can be viewed by any browser. In order to get the full effect you must
place the DLogger dltheme folder that is included in the Unity package to the location where you
have requested to put your log files.


Pragma Directives
=================

Pragmas are definitions you can put in your files that can turn on code when they are present. You can
do this for each file individually, or globally using a global #define file in the unity Assets folder.

	http://forum.unity3d.com/threads/71445-How-To-Set-Project-Wide-pragma-Directives-with-JavaScript

Prime31 has created a wonderful tool for creating global #define's (it as been included in this package as a convenience):

	https://github.com/prime31/P31UnityAddOns/blob/master/Editor/GlobalDefinesWizard.cs	
	
It is the developers choice as to which method to use. See the D.cs class for instructions on how to turn
on features on the logger, and what each feature means. Here is a summary:

DEBUG_LEVEL_LOG			shows all messages including warnings and errors
DEBUG_LEVEL_WARN		only show warnings and errors
DEBUG_LEVEL_ERROR		only shows errors

DEBUG_TOFILE			dumps all logs to an external TEXT file
DEBUG_TOCONSOLE			dumps all logs to the Unity console
DEBUG_TOHTML			dumps all logs to an HTML file

DLogger Prefab
==============

In the DLogger folder, there is a Prefab call DLogger. You can control some of the logging parameters by changing
the values in the Editor. Please make sure you do not RENAME this GameObject.

Note that a DLogger GameObject will also be created in your scene whenever you call method in the D class. This is 
by design, however it will NOT destroy one you may have already put in the scene.

Inspiration
===========

I want to thank the following for their inspiration for this code.

Prime31 (Mike) for the original D.cs class for logging in Unity.

	https://github.com/prime31

DevMag for their articles on creating an external log with html formatting.

	http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/
	http://devmag.org.za/2011/01/25/make-your-logs-interactive-and-squash-bugs-faster/
	

Licenses
========

Silk icon set 1.3

_________________________________________
Mark James
http://www.famfamfam.com/lab/icons/silk/
_________________________________________

This work is licensed under a
Creative Commons Attribution 2.5 License.
[ http://creativecommons.org/licenses/by/2.5/ ]

This means you may use it for any purpose,
and make any changes you like.
All I ask is that you include a link back
to this page in your credits.

Are you using this icon set? Send me an email
(including a link or picture if available) to
mjames@gmail.com

Any other questions about this icon set please
contact mjames@gmail.com