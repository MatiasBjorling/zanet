using System;
using System.Reflection;
using System.Runtime.CompilerServices;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: AssemblyVersion("0.9.2")]

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//       When specifying the KeyFile, the location of the KeyFile should be
//       relative to the project output directory which is
//       %Project Directory%\obj\<configuration>. For example, if your KeyFile is
//       located in the project directory, you would specify the AssemblyKeyFile 
//       attribute as [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]

#region Helper class 

// Your generated class does not contain the follwoing part.
/// <summary>
/// 
/// </summary>
public class AssemblyInfo
{
	// Used by Helper Functions to access information from Assembly Attributes

	/// <summary>
	/// 
	/// </summary>
	private Type myType;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	public AssemblyInfo(Type type)
	{
		myType = type;
	}
	/// <summary>
	/// 
	/// </summary>
	public string AsmName 
	{
		get {return myType.Assembly.GetName().Name.ToString();}
	}
	/// <summary>
	/// 
	/// </summary>
	public string AsmFQName 
	{
		get {return myType.Assembly.GetName().FullName.ToString();}
	}
	/// <summary>
	/// 
	/// </summary>
	public string CodeBase 
	{
		get {return myType.Assembly.CodeBase;}
	}
	/// <summary>
	/// 
	/// </summary>
	public string Copyright 
	{
		get 
		{
			Type at = typeof(AssemblyCopyrightAttribute);
			object[] r = myType.Assembly.GetCustomAttributes(at, false);
			AssemblyCopyrightAttribute ct = (AssemblyCopyrightAttribute) r[0];
			return ct.Copyright;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public string Company 
	{
		get 
		{
			Type at = typeof(AssemblyCompanyAttribute);
			object[] r = myType.Assembly.GetCustomAttributes(at, false);
			AssemblyCompanyAttribute ct = (AssemblyCompanyAttribute) r[0];
			return ct.Company;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string Description 
	{
		get 
		{
			Type at = typeof(AssemblyDescriptionAttribute);
			object[] r = myType.Assembly.GetCustomAttributes(at, false);
			AssemblyDescriptionAttribute da = (AssemblyDescriptionAttribute) r[0];
			return da.Description;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public string Product 
	{
		get 
		{
			Type at = typeof(AssemblyProductAttribute);
			object[] r = myType.Assembly.GetCustomAttributes(at, false);
			AssemblyProductAttribute pt = (AssemblyProductAttribute) r[0];
			return pt.Product;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public string Title 
	{
		get 
		{
			Type at = typeof(AssemblyTitleAttribute);
			object[] r = myType.Assembly.GetCustomAttributes(at, false);
			AssemblyTitleAttribute ta = (AssemblyTitleAttribute) r[0];
			return ta.Title;
		}
	}
	/// <summary>
	/// 
	/// </summary>
	public string Version 
	{
		get {return myType.Assembly.GetName().Version.ToString();}
	}
}
#endregion

