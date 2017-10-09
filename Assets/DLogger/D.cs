// Portions are Copyright 2011-2013, SpockerDotNet LLC http://www.sdnllc.com/
// Version 0.1

/**
 * Orignial version of this Script can be found at:
 * 
 * 		https://github.com/prime31/P31UnityAddOns/blob/master/Scripts/Debugging/D.cs
 * 
 * 
 * 
 * We suggest that you use the Global Defines wizard from Prime31 to manage these
 * 
 * 		https://github.com/prime31/P31UnityAddOns/blob/master/Editor/GlobalDefinesWizard.cs
 * 
 * 
 * 
 * To turn on a feature, simply uncomment the #define you want to activate. 
 * To turn it off, simply recomment the line.
 * 
 * Note that a recompile is required before the affect will take place.
 * 
 **/

#define DEBUG_LEVEL_LOG
//#define DEBUG_LEVEL_WARN
//#define DEBUG_LEVEL_ERROR
#define DEBUG_TOFILE
//#define DEBUG_TOCONSOLE
#define DEBUG_TOHTML

using UnityEngine;
using System.Collections;
using System.IO;


public static class D
{
	private static StreamWriter 	m_writer;
	private static StreamWriter 	m_html;
	
	private static DLogger			m_logger;
	
	private static string			m_logPath;
	private static string			m_logName;
	
	static D()
	{
		//Debug.Log("D");
		
		GameObject _logger = GameObject.Find("DLogger");
		
		if ( _logger == null ) {
			//Debug.Log("Adding ULogger GameObject");
			_logger = new GameObject();
			_logger.name = "DLogger";
			_logger.AddComponent<DLogger>();
		}
		
		m_logger = _logger.GetComponent<DLogger>();
		
		m_logPath = Application.dataPath + "\\";
		m_logName = "dlogger";
		
		if ( m_logger.LoggerPath != "" ) {
			m_logPath = m_logger.LoggerPath;
		}
		
		if ( m_logger.LoggerName != "" ) {
			m_logName = m_logger.LoggerName;
		}
		
		//Debug.Log(m_logPath + m_logName);

		CreateLogFile();		
		CreateHtmlLogFile();
		
		Application.RegisterLogCallback( logCallback );
	}
	
	
	public static void logCallback( string log, string stackTrace, LogType type )
	{
		// error gets a stack trace
		if( type == LogType.Error )
		{
			System.Console.WriteLine( log );
			System.Console.WriteLine( stackTrace );
		}
		else
		{
			System.Console.WriteLine( log );
		}
	}
	
	
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_LOG" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_WARN" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_ERROR" )]
	public static void log( object format, params object[] paramList )
	{
		if( format is string ) {
			LogToConsole(string.Format( format as string, paramList ) );
			LogToFile(string.Format( format as string, paramList ) );
			LogToHtml("Log", string.Format( format as string, paramList ) );
		}
		else {
			LogToConsole( format);
			LogToFile( format );
			LogToHtml( "Log", format );
		}
	}
	
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_LOG" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_WARN" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_ERROR" )]
	public static void log( string type, object format, params object[] paramList )
	{
		if( format is string ) {
			LogToConsole(string.Format( format as string, paramList ) );
			LogToFile(string.Format( format as string, paramList ) );
			LogToHtml(type, string.Format( format as string, paramList ) );
		}
		else {
			LogToConsole( format);
			LogToFile( format );
			LogToHtml( type, format );
		}
	}
	
	
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_WARN" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_ERROR" )]
	public static void warn( object format, params object[] paramList )
	{
		if( format is string ) {
			LogToConsole(string.Format( format as string, paramList ) );
			LogToFile(string.Format( format as string, paramList ) );
			LogToHtml("Warning", string.Format( format as string, paramList ) );
		}
		else {
			LogToConsole( format);
			LogToFile( format );
			LogToHtml( "Warning", format );
		}
	}
	
	
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_ERROR" )]
	public static void error( object format, params object[] paramList )
	{
		if( format is string ) {
			LogToConsole(string.Format( format as string, paramList ) );
			LogToFile(string.Format( format as string, paramList ) );
			LogToHtml("Error", string.Format( format as string, paramList ) );
		}
		else {
			LogToConsole( format);
			LogToFile( format );
			LogToHtml( "Error", format );
		}
	}
	
	[System.Diagnostics.Conditional( "UNITY_EDITOR" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_LOG" )]
	public static void assert( bool condition )
	{
		assert( condition, string.Empty, true );
	}

	
	[System.Diagnostics.Conditional( "UNITY_EDITOR" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_LOG" )]
	public static void assert( bool condition, string assertString )
	{
		assert( condition, assertString, false );
	}

	
	[System.Diagnostics.Conditional( "UNITY_EDITOR" )]
	[System.Diagnostics.Conditional( "DEBUG_LEVEL_LOG" )]
	public static void assert( bool condition, string assertString, bool pauseOnFail )
	{
		if( !condition )
		{
			Debug.LogError( "assert failed! " + assertString );
			
			if( pauseOnFail )
				Debug.Break();
		}
	}
	
	[System.Diagnostics.Conditional( "DEBUG_TOFILE" )]
	private static void CreateLogFile() {
		//Debug.Log("CreateLogFile");
		m_writer = new StreamWriter( m_logPath + m_logName + ".log", false );
		m_writer.AutoFlush = true;
		LogToFile("Logger Active...");
	}

	[System.Diagnostics.Conditional( "DEBUG_TOFILE" )]
	private static void CloseLogFile() {
		//Debug.Log("CloseLogFile");
		m_writer.Close();
	}
		
	[System.Diagnostics.Conditional( "DEBUG_TOHTML" )]
	private static void CreateHtmlLogFile() {
		//Debug.Log("CreateHtmlLogFile");
		m_html = new StreamWriter( m_logPath + m_logName + ".html", false );
		m_html.AutoFlush = true;
		m_html.WriteLine("<head>");
		m_html.WriteLine("<script language=\"javascript\" src=\"dlstyle\\dlogger.javascript\"></script>");
		m_html.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"dlstyle\\dlogger.css\" />");
		m_html.WriteLine("</head>");
	}
	
	[System.Diagnostics.Conditional( "DEBUG_TOHTML" )]
	private static void CloseHtmlLogFile() {
		//Debug.Log("CloseHtmlLogFile");
		m_html.Close();
	}
	
	[System.Diagnostics.Conditional( "DEBUG_TOCONSOLE" )]
	private static void LogToConsole(object log) {
		Debug.Log(log);
	}
	
	[System.Diagnostics.Conditional( "DEBUG_TOFILE" )]
	private static void LogToFile(object log) {
		string _val = log as string;
		string _log = string.Format("{0} {1} {2}", System.DateTime.Now.ToFileTime(), "LOG", _val);
		m_writer.WriteLine(_log);
	}

	[System.Diagnostics.Conditional( "DEBUG_TOHTML" )]
	private static void LogToHtml(string type, object log) {
		m_html.Write("<p class=\"" + type + "\">");
		m_html.Write("<span class=\"Icon\"><img src=\"dlstyle\\{0}.png\" title=\"" + type + "\"></span><span class=\"Time\">{1}</span>", type.ToLower(), System.DateTime.Now.ToString("MM/dd/yyy hh:mm:ss.fff"));
		string _val = log as string;
		string _log = string.Format(_val);
		m_html.Write(_log);
		m_html.Write("</p>");
	}
	
	public static void Quit() {
		//Debug.Log("DLogger is Shutting Down...");
		CloseHtmlLogFile();
		CloseLogFile();
	}

}