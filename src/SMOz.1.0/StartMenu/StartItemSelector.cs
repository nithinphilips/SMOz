using System;
using System.Collections.Generic;
using System.Text;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;

namespace SMOz.StartMenu
{
    public class StartItemSelector
    {
	   public enum StartItemFields { Application, Category, HasLocal, HasUser, Location, Name, RealName, Type }

	   StartItem[] source;
	   StartItemFields field;
	   object query;

	   public object Query {
		  get { return this.query; }
	   }

	   public SMOz.StartMenu.StartItemSelector.StartItemFields Field {
		  get { return this.field; }
	   }

	   public SMOz.StartMenu.StartItem[] Source {
		  get { return this.source; }
	   }

	   public StartItemSelector(StartItem[] source, StartItemFields field, object query) {
		  this.source = source;
		  this.field = field;
		  this.query = query;
	   }

	   	 

	   // This is too inefficient
	   public void Execute() {

		  string sourceCodeFormat = @"
			 using System;
			 using System.Collections.Generic;
			 using SMOz.StartMenu;
    		  
			 namespace SMOz.StartMenu{{
				public class GenericSelector{{
				   public static StartItem[] Select(StartItem[] source, {0} query){{
					   List<StartItem> result = new List<StartItem>(source.Length);
					   for (int i = 0; i < source.Length; i++) {{
						  if (source[i].{1} == query) {{
							 result.Add(source[i]);
						  }}
					   }}
					   return result.ToArray();	
				    }}
				}}
			 }}
		  ";

		  string sourceCode = string.Format(sourceCodeFormat, "string", field.ToString());

		  CSharpCodeProvider provider = new CSharpCodeProvider();

		  // Build the parameters for source compilation.
		  CompilerParameters cp = new CompilerParameters();
		  cp.ReferencedAssemblies.Add("System.dll");
		  cp.ReferencedAssemblies.Add("SMOz.exe");
		  cp.GenerateInMemory = true;

		  // Invoke compilation.
		  CompilerResults cr = provider.CompileAssemblyFromSource(cp, sourceCode);

		  if (cr.Errors.Count > 0) {
			 // Display compilation errors.
			 Console.WriteLine("Errors building code:");
			 foreach (CompilerError ce in cr.Errors) {
				Console.WriteLine("  {0}", ce.ToString());
				Console.WriteLine();
			 }
		  } else {
			 object genericSelector = cr.CompiledAssembly.CreateInstance("SMOz.StartMenu.GenericSelector");
			 if (genericSelector != null) {

				object[] genParams = new object[] { source, query };

				object result = genericSelector.GetType().InvokeMember("Select", BindingFlags.InvokeMethod, null, genericSelector, genParams);

				StartItem[] realResult = (StartItem[])result;

				foreach (StartItem item in realResult) {
				    Console.WriteLine(item.Name);
				}
			 }
		  }
	   }

	   /*
	   public StartItem[] Execute() {
		  List<StartItem> result = new List<StartItem>(source.Length);
		  switch (field) {
			 case StartItemFields.Application: {
				    string query = this.query as string;
				    for (int i = 0; i < source.Length; i++) {
					   if (source[i].Application.CompareTo(query) == 0) {
						  result.Add(source[i]);
					   }
				    }
				}
				break;
			 case StartItemFields.Category:
				break;
			 case StartItemFields.HasLocal:
				break;
			 case StartItemFields.HasUser:
				break;
			 case StartItemFields.Location:
				break;
			 case StartItemFields.Name:
				break;
			 case StartItemFields.RealName:
				break;
			 case StartItemFields.Type:
				break;
			 default:
				throw new ArgumentException("The field is not supported");
		  }
		  return result.ToArray();
	   }
	    */

    }
}
