# GraphQL Hot Chocolate mutations as functions?

Experiment! My goal is to write business logic as functions, like it's F# but with C#. This is very adhoc way to write application server, not for everyone.

`BusinessLogic` directory contains each GraphQL mutation as a function. Since C# (or Hot Chocolate) doesn't allow writing just functions, I decided to extend single partial class `BusinessLogic`. 

Number one rule is that these business logic functions must have fully qualified dependencies on the parameters. This way they are easily rewritable in F# when Hot Chocolate is ready for it.

