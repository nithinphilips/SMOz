The Logger example, created by https://github.com/Wattos, demostrates adding logging instrumentation 
to a target assembly as a post-build step.  Note that the target assembly has a compile-time dependency
on the Logger core project, but does not reference Afterthought, even after the amending process has occurred.
The Logger.Amender project instructs Afterthought as to how to modify target assemblies, and is passed in
as a second argument when the target assembly is amended.  The resulting assembly retains just a simple
reference to Logger.