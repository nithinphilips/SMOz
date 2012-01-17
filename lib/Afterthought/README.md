Afterthought allows developers to post-process .NET assemblies to add code that either cannot be added to the original source or is not convenient/efficient to do so.  Examples include:

1. Tweaking a compiled assembly for which you do not have the source code be must support/modify
2. Adding instrumentation logic to an assembly for test purposes, which will not always be part of the release version
3. Implementing tedious interfaces or patterns that get in the way of the simplicity of your coding efforts when directly implemented.

We developed Afterthought specifically based on our experience with PostSharp.  While we enjoyed PostSharp and the advantages it provides, this product has two downsides:

1. The developer experience is a bit complicated, introducing new concepts like aspects and generally requiring you to understand how PostSharp works internally to make it work for you.  It required you to frequently cast objects to known types instead of writing fluent strongly-typed intuitive code.  Lastly, the emitted code to do something very simple is very complex--dozens of lines of code just to call one line of your own code.

2. PostSharp is no longer free.  We have been developing open-source libraries for the past couple of years, and as we prepared to release them publically, we realized that some dependencies on PostSharp made our own open source efforts less appealing.  The introduction of Afterthought eliminates this issue by providing a completely free option.

So, what does Afterthought do?  Quite simply, it allows you to:

1. Create type amendments by subclassing Amendment<,> to describe what changes you want to make
2. Add attributes to your types indicating which types to amend and what amendments to apply
3. Amend properties by either adding new properties or modifying the logic of existing properties
4. Amend methods by either adding new methods or modifying the logic of existing methods
5. Amend constructors by either adding new constructors or modifying the logic of existing constructors
6. Implement interfaces directly on a type leveraging existing properties/methods, adding new properties/methods, and allowing Afterthought to automatically implement everything else
7. Configure your project to call Afterthought as a post-build step to apply the changes