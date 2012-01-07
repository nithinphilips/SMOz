/*************************************************************************
 *  SMOz (Start Menu Organizer)
 *  Copyright (C) 2006 Nithin Philips
 *
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU General Public License
 *  as published by the Free Software Foundation; either version 2
 *  of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program; if not, write to the Free Software
 *  Foundation,Inc.,59 Temple Place - Suite 330,Boston,MA 02111-1307, USA.
 *
 *  Author            :  Nithin Philips <spikiermonkey@users.sourceforge.net>
 *  Original FileName :  StartCategorizer.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using SMOz.Commands.UI;
using SMOz.Template;
using System.Globalization;
using System.Diagnostics;

namespace SMOz.StartMenu
{
    /// <summary>
    /// Generates a set of operations to be applied to StartItems based on a ICategoryProvider.
    /// This may not work properly as I have confused myself with complicated rules. Dammit!
    /// </summary>
    public class StartCategorizer
    {
	   public static MoveStartItemCommand[] Categorize(ICategoryProvider template, StartManager start) {
		  return Categorize(template, start, "", true, false);
	   }

	   public static MoveStartItemCommand[] Categorize(ICategoryProvider template, StartManager start, string directory) {
		  return Categorize(template, start, directory, false, false);
	   }

	   public static MoveStartItemCommand[] Categorize(ICategoryProvider template, StartManager start, string directory, bool forceAllFilters) {
		  return Categorize(template, start, directory, false, forceAllFilters);
	   }

	   private static MoveStartItemCommand[] Categorize(ICategoryProvider template, StartManager start, string category, bool rootAndRestricted, bool forceAllFilters) {
		  List<MoveStartItemCommand> result = new List<MoveStartItemCommand>();

		  IEnumerable<StartItem> items;
		  if (rootAndRestricted) {
			 items = start.StartItems;
		  } else {
			 items = start.GetByCategory(category);
		  }

		  foreach (StartItem currentItem in items) {
			 Category predicted = template.FindCategory(currentItem.Name);
			 bool proceed = false;

			 if (predicted != null) {
				if (string.IsNullOrEmpty(currentItem.Category)) {
				    // Item is located in the root folder

				    // Proceed unless the filter is restricted
				    proceed = !predicted.IsRestricted;

				    // Proceed anyways if forced
				    if (!proceed && forceAllFilters) {
					   proceed = true;
					   Debug.WriteLine("[Restricted: FORCED]" + currentItem.Name);
				    } else {
					   Debug.WriteLine("[Empty: " + (proceed ? "OK]" : "SKIP]") + currentItem.Name);
				    }
				} else {
				    // Item is not in the root folder
				    bool restricted = !string.IsNullOrEmpty(predicted.RestrictedPath);

				    if (restricted && (string.Compare(currentItem.Category, predicted.RestrictedPath, true) == 0)) {
					   // Item is located in a restricted folder; which is handled by the predicted filter
					   Debug.WriteLine("[Restricted: OK] " + currentItem.Name);
					   proceed = true;
				    } else {
					   // Item is NOT in a restricted folder or the root

					   if (!rootAndRestricted) {
						  // Items are from a subset; 
						  if(forceAllFilters){
							 Debug.WriteLine("[Not In Root: FORCED]" + currentItem.Name);
							 proceed = true;
						  }else if(!restricted){
							 Debug.WriteLine("[Not In Root: NOT RESTRICTED; OK]" + currentItem.Name);
							 proceed = true;
						  }else{
							 Debug.WriteLine("[Not In Root: RESTRICTED; SKIP]" + currentItem.Name);
							 proceed = false;
						  }						  
					   } else {				  
						  // never applied
						  Debug.WriteLine("[Not In Root: SKIP]" + currentItem.Name);
						  proceed = false;
					   }
				    }
				}
			 }

		      if (proceed) {
				result.Add(new MoveStartItemCommand(currentItem, predicted.Name));
			 }
		  }

		  return result.ToArray();
	   }
    }
}
