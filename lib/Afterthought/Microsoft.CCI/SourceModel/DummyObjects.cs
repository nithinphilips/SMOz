﻿//-----------------------------------------------------------------------------
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the Microsoft Public License.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
//^ using Microsoft.Contracts;

namespace Microsoft.Cci {

#pragma warning disable 1591

  public static class SourceDummy {

    public static ICompilation Compilation {
      get {
        if (SourceDummy.compilation == null)
          Interlocked.CompareExchange(ref SourceDummy.compilation, new DummyCompilation(), null);
        return SourceDummy.compilation;
      }
    }
    private static ICompilation/*?*/ compilation;

    public static IPrimarySourceDocument PrimarySourceDocument {
      get {
        if (SourceDummy.primarySourceDocument == null)
          Interlocked.CompareExchange(ref SourceDummy.primarySourceDocument, new DummyPrimarySourceDocument(), null);
        return SourceDummy.primarySourceDocument;
      }
    }
    private static IPrimarySourceDocument/*?*/ primarySourceDocument;

    /// <summary>
    /// This source location behaves as the notorious FeeFee: a source
    /// context which exists in the PDB file, but which debuggers do not
    /// stop at. It is needed, e.g., to mark the first IL operation in a
    /// method body if it does not have any real source location (such as
    /// compiler generated closure initialization code). Without that, the
    /// debugger appears to not step into method calls.
    /// </summary>
    public static IPrimarySourceLocation PrimarySourceLocation {
      get {
        if (SourceDummy.primarySourceLocation == null)
          Interlocked.CompareExchange(ref SourceDummy.primarySourceLocation, new DummyPrimarySourceLocation(), null);
        return SourceDummy.primarySourceLocation;
      }
    }
    private static IPrimarySourceLocation/*?*/ primarySourceLocation;

    public static ISourceDocument SourceDocument {
      get {
        if (SourceDummy.sourceDocument == null)
          Interlocked.CompareExchange(ref SourceDummy.sourceDocument, new DummySourceDocument(), null);
        return SourceDummy.sourceDocument;
      }
    }
    private static ISourceDocument/*?*/ sourceDocument;

    public static ISourceDocumentEdit SourceDocumentEdit {
      get {
        if (SourceDummy.sourceDocumentEdit == null)
          Interlocked.CompareExchange(ref SourceDummy.sourceDocumentEdit, new DummySourceDocumentEdit(), null);
        return SourceDummy.sourceDocumentEdit;
      }
    }
    private static ISourceDocumentEdit/*?*/ sourceDocumentEdit;

    public static ISourceLocation SourceLocation {
      get {
        if (SourceDummy.sourceLocation == null)
          Interlocked.CompareExchange(ref SourceDummy.sourceLocation, new DummySourceLocation(), null);
        return SourceDummy.sourceLocation;
      }
    }
    private static ISourceLocation/*?*/ sourceLocation;

  }

  internal sealed class DummyCompilation : ICompilation {

    #region ICompilation Members

    public IPlatformType PlatformType {
      get { return Dummy.PlatformType; }
    }

    public bool Contains(ISourceDocument sourceDocument) {
      return false;
    }

    public IUnitSet GetUnitSetFor(IName unitSetName) {
      return Dummy.UnitSet;
    }

    public IUnit Result {
      get { return Dummy.Unit; }
    }

    public IUnitSet UnitSet {
      get { return Dummy.UnitSet; }
    }

    #endregion
  }

  internal sealed class DummyPrimarySourceDocument : IPrimarySourceDocument {

    #region IPrimarySourceDocument Members

    Guid IPrimarySourceDocument.DocumentType {
      get { return Guid.Empty; }
    }

    Guid IPrimarySourceDocument.Language {
      get { return Guid.Empty; }
    }

    Guid IPrimarySourceDocument.LanguageVendor {
      get { return Guid.Empty; }
    }

    IPrimarySourceLocation IPrimarySourceDocument.PrimarySourceLocation {
      get { return SourceDummy.PrimarySourceLocation; }
    }

    IPrimarySourceLocation IPrimarySourceDocument.GetPrimarySourceLocation(int position, int length) {
      return SourceDummy.PrimarySourceLocation;
    }

    void IPrimarySourceDocument.ToLineColumn(int position, out int line, out int column) {
      line = 0;
      column = 0;
    }

    #endregion

    #region ISourceDocument Members

    int ISourceDocument.CopyTo(int position, char[] destination, int destinationOffset, int length) {
      return 0;
    }

    ISourceLocation ISourceDocument.GetCorrespondingSourceLocation(ISourceLocation sourceLocationInPreviousVersionOfDocument) {
      return SourceDummy.PrimarySourceLocation;
    }

    //^ [Pure]
    ISourceLocation ISourceDocument.GetSourceLocation(int position, int length) {
      return SourceDummy.PrimarySourceLocation;
    }

    string ISourceDocument.GetText() {
      return "";
    }

    //^ [Confined]
    bool ISourceDocument.IsUpdatedVersionOf(ISourceDocument sourceDocument) {
      return false;
    }

    int ISourceDocument.Length {
      get { return 0; }
    }

    string ISourceDocument.SourceLanguage {
      get { return ""; }
    }

    ISourceLocation ISourceDocument.SourceLocation {
      get { return SourceDummy.PrimarySourceLocation; }
    }

    #endregion

    #region IDocument Members

    string IDocument.Location {
      get { return ""; }
    }

    IName IDocument.Name {
      get { return Dummy.Name; }
    }

    #endregion

  }

  /// <summary>
  /// Instances of this class behave as the notorious FeeFee: a source
  /// context which exists in the PDB file, but which debuggers do not
  /// stop at. It is needed, e.g., to mark the first IL operation in a
  /// method body if it does not have any real source location (such as
  /// compiler generated closure initialization code). Without that, the
  /// debugger appears to not step into method calls.
  /// </summary>
  internal sealed class DummyPrimarySourceLocation : IPrimarySourceLocation {

    #region IPrimarySourceLocation Members

    int IPrimarySourceLocation.EndColumn {
      get { return 0; }
    }

    int IPrimarySourceLocation.EndLine {
      get { return 0x00feefee; }
    }

    IPrimarySourceDocument IPrimarySourceLocation.PrimarySourceDocument {
      get { return SourceDummy.PrimarySourceDocument; }
    }

    int IPrimarySourceLocation.StartColumn {
      get { return 0; }
    }

    int IPrimarySourceLocation.StartLine {
      get { return 0x00feefee; }
    }

    #endregion

    #region ISourceLocation Members

    //^ [Pure]
    bool ISourceLocation.Contains(ISourceLocation location) {
      return false;
    }

    //^ [Pure]
    int ISourceLocation.CopyTo(int offset, char[] destination, int destinationOffset, int length) {
      return 0;
    }

    int ISourceLocation.EndIndex {
      get { return 0; }
    }

    int ISourceLocation.Length {
      get { return 0; }
    }

    ISourceDocument ISourceLocation.SourceDocument {
      get { return SourceDummy.PrimarySourceDocument; }
    }

    string ISourceLocation.Source {
      get { return ""; }
    }

    int ISourceLocation.StartIndex {
      get { return 0; }
    }

    #endregion

    #region ILocation Members

    IDocument ILocation.Document {
      get { return SourceDummy.PrimarySourceDocument; }
    }

    #endregion
  }

  internal sealed class DummySourceDocument : ISourceDocument {
    #region ISourceDocument Members

    public int CopyTo(int position, char[] destination, int destinationOffset, int length) {
      return 0;
    }

    public ISourceLocation GetCorrespondingSourceLocation(ISourceLocation sourceLocationInPreviousVersionOfDocument) {
      return SourceDummy.SourceLocation;
    }

    //^ [Pure]
    public ISourceLocation GetSourceLocation(int position, int length) {
      //^ assume false;
      return SourceDummy.SourceLocation;
    }

    public string GetText() {
      //^ assume false;
      return string.Empty;
    }

    //^ [Confined]
    public bool IsUpdatedVersionOf(ISourceDocument sourceDocument) {
      return sourceDocument == SourceDummy.SourceDocument;
    }

    public string Location {
      get { return string.Empty; }
    }

    public int Length {
      get
        //^ ensures result == 0;
      {
        return 0;
      }
    }

    public IName Name {
      get { return Dummy.Name; }
    }

    public string SourceLanguage {
      get { return string.Empty; }
    }

    public ISourceLocation SourceLocation {
      get { return SourceDummy.SourceLocation; }
    }

    #endregion

  }

  internal sealed class DummySourceDocumentEdit : ISourceDocumentEdit {

    #region ISourceDocumentEdit Members

    public ISourceLocation SourceLocationBeforeEdit {
      get { return SourceDummy.SourceLocation; }
    }

    public ISourceDocument SourceDocumentAfterEdit {
      get {
        //^ assume false;
        return SourceDummy.SourceDocument;
      }
    }

    #endregion

  }

  internal sealed class DummySourceLocation : ISourceLocation {

    #region ISourceLocation Members

    //^ [Pure]
    public bool Contains(ISourceLocation location) {
      return false;
    }

    //^ [Pure]
    public int CopyTo(int offset, char[] destination, int destinationOffset, int length) {
      //^ assume false;
      return 0;
    }

    public int EndIndex {
      get {
        //^ assume false;
        return 0;
      }
    }

    public int Length {
      get
        //^ ensures result == 0;
      {
        //^ assume false;
        return 0;
      }
    }

    public ISourceDocument SourceDocument {
      get { return SourceDummy.SourceDocument; }
    }

    public string Source {
      get {
        //^ assume false;
        return string.Empty;
      }
    }

    public int StartIndex {
      get { return 0; }
    }

    #endregion

    #region ILocation Members

    public IDocument Document {
      get { return SourceDummy.SourceDocument; }
    }

    #endregion
  }

#pragma warning restore 1591
}
