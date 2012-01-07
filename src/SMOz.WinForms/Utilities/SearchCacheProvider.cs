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
 *  Original FileName :  SearchCacheProvider.cs
 *  Created           :  Fri Mar 31 2006
 *  Description       :  
 *************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SMOz.Utilities
{
    public class SearchCacheProvider<TKey, UValue>{

	   Dictionary<TKey, UValue> cacheTable;
	   Dictionary<TKey, int> preferenceTable;
	   int limit;

	   public SearchCacheProvider() : this(10) { }

	   public SearchCacheProvider(int limit) {
		  this.limit = limit;
		  this.cacheTable = new Dictionary<TKey, UValue>(limit);
		  this.preferenceTable = new Dictionary<TKey, int>(limit);
	   }

	   public void AddResults(TKey key, UValue searchResults) {
		  if (this.cacheTable.Count >= limit) {
			 TKey leastPref = GetLeastPreferred();
			 this.cacheTable.Remove(leastPref);
			 this.preferenceTable.Remove(leastPref);
		  }
		  this.cacheTable.Add(key, searchResults);
		  this.preferenceTable.Add(key, 0);
	   }

	   // ? - Does this work
	   private TKey GetLeastPreferred() {
		  int hitCount = -1;
		  TKey result = default(TKey);
		  foreach (KeyValuePair<TKey, int> pair in preferenceTable) {
			 if ((pair.Value < hitCount) || (hitCount == -1)) {
				result = pair.Key;
				hitCount = pair.Value;
			 }
		  }
		  return result;
	   }

	   public UValue GetCachedResults(TKey key) {
		  this.preferenceTable[key]++;
		  return this.cacheTable[key];
	   }

	   public bool HasCache(TKey key) {
		  return this.cacheTable.ContainsKey(key);
	   }

	   public void Invalidate(TKey key) {
		  this.cacheTable.Remove(key);
		  this.preferenceTable.Remove(key);
	   }

	   public void Invalidate() {
		  this.cacheTable.Clear();
		  this.preferenceTable.Clear();
	   }

    }
}
