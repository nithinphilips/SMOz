using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using System.Diagnostics.Contracts;

namespace Logging.UnitTest.Target
{
    /// <summary>
    /// A Beer class :)
    /// </summary>
    [LoggingAttribute]
    public class Beer
    {

        /// <summary>
        /// Invariant for a beer
        /// </summary>
        [DisableLogging] //dont log invariant methods. Thats not smart
        [ContractInvariantMethod]
        private void Invariant()
        {

            //Note, the approach used is A implies B <==> !A || B
            Contract.Invariant(!Full || !Dropped, "A beer cannot be dropped if it is full" );
            Contract.Invariant(!Dropped || !Full, "A beer cannot be full if it was dropped");
            Contract.Invariant(!Full || !Empty, "A beer cannot be empty if its full");
            Contract.Invariant(!Empty || !Full, "A beer cannot be full it it is empty");
        }

        /// <summary>
        /// Private beer constructor
        /// </summary>
        public Beer()
        {
             Dropped = false;
             Full = true;
        }

        /// <summary>
        /// Did we drop the beer?
        /// </summary>
        public bool Dropped { [Pure] get; private set; }

        /// <summary>
        /// Is the beer empty?
        /// </summary>
        public bool Empty { [Pure] get; private set; }

        /// <summary>
        /// Is the beer full?
        /// </summary>
        public bool Full { [Pure] get { return !Empty; } private set { Empty = !value; } }

        /// <summary>
        /// Create a new Beer instance
        /// </summary>
        public static Beer Pour()
        {
            Contract.Ensures(Contract.Result<Beer>().Full, "Returned beer must be full");
            Contract.Ensures(!Contract.Result<Beer>().Dropped, "Returned beer must not be dropped");

            return new Beer { };
        }

        /// <summary>
        /// Drink A beer
        /// </summary>
        public void Drink()
        {
            Contract.Requires(Full, "Can only drink a beer which is full");
            Contract.Requires(Dropped, "Cannot drink a beer which was dropped");
            Contract.Ensures(Empty, "Beer must be empty after drinking");

            Empty = true;
        }

        /// <summary>
        /// Never drop a beer!!!!
        /// </summary>
        public void Drop(bool lucky)
        {
            Contract.Requires(!Dropped, "Beer cannot be dropped already");
            Contract.Ensures(lucky ^ Dropped, "If lucky, beer was not dropped, otherwise it was dropped");
            Dropped = !lucky;
            Empty = Dropped;
        }
    }
}
