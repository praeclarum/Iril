using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Iril.IR;
using Iril.Types;

namespace Iril
{
    /// <summary>
    /// Collection of modules
    /// </summary>
    public class Library
    {
        public Symbol Symbol { get; }
        public Module[] Modules { get; }

        public Library (Symbol symbol, Module[] modules)
        {
            Symbol = symbol;
            Modules = modules ?? throw new ArgumentNullException (nameof (modules));
        }

        public static Library FromZip (Symbol librarySymbol, Stream stream)
        {
            var zip = new ZipArchive (stream, ZipArchiveMode.Read);
            var llentries = zip.Entries.Where (x => x.Name.EndsWith (".ll", StringComparison.InvariantCultureIgnoreCase));
            var modules = llentries.Select (ReadModule).Where (x => x != null).ToArray ();

            return new Library (librarySymbol, modules);

            Module ReadModule (ZipArchiveEntry entry)
            {
                using (var s = entry.Open ()) {
                    using (var r = new StreamReader (s)) {
                        var name = Path.Combine (librarySymbol.Text, entry.Name);
                        var llvm = r.ReadToEnd ();
                        try {
                            var m = Module.Parse (llvm, name);
                            foreach (var e in m.Errors) {
                                Debug.WriteLine (e);
                            }
                            return m;
                        }
                        catch (Exception ex) {
                            Debug.WriteLine (ex);
                            return null;
                        }
                    }
                }
            }
        }

        static readonly Task<SymbolTable<Library>> standardLibraries;

        public static SymbolTable<Library> StandardLibraries => standardLibraries.Result;

        public static Task LoadStandardLibrariesAsync ()
        {
            return standardLibraries;
        }

        static Library()
        {
            standardLibraries = LoadStandardLibraries ();
        }

        static Task<SymbolTable<Library>> LoadStandardLibraries ()
        {
            return Task.Run (() => {
                var r = new SymbolTable<Library> ();

                var asm = typeof (Library).Assembly;

                //var libcxx = Library.FromZip ("libcxx", asm.GetManifestResourceStream ("Iril.Lib.libcxx.zip"));
                //r[libcxx.Symbol] = libcxx;

                return r;
            });
        }
    }

}
