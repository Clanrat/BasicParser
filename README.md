# BasicParser
Basic parser for evaluating mathematical functions in string form

Uses the railroad shunt algortithm to convert the expression to RPN which is then evaluated

## Usage
```C#
var parser = new SimpleParser();
var result = parser.Parse("1+3");
```
Output:
`4`

Using the symbolic evaluator
```C#
var parser = new SimpleParser();
var result = parser.Parse("1+3+5*x","x","5");
```
Output:
`29`
## Features

* Very modular, operators are added at runtime by instanciating any IOperator class
* Use any converter or parser you want, as long as it extends the interfaces. 
* Symbolic evaluator for any amount of symbols


## Adding new operators:
To add new operators all that is needed is to instanciate any derived type of the `IOperator<T>` interface

```C#

var addition = new Operator<double>("+", 1, Assosiativity.B, (a, b) => a + b, true, (a) => +a);
```
`symbol` : the symbol that you want your operator to be represented by can be any char except: , . ( ) or any digit

`precendense` : operator precedense, the higher the number the higher the precedense

`associativity` : If it is left, right assosiative or both.

`function` : What will the operator return when `.Evaluate()` is called

`specialUnary` : Does the operator have a special behaviour when called as a unary operator (like '-') (doesn't need to be set for 
purely unary operators)

`unaryFunc` : if specialUnary is true, what will the operator return when `.Evaluate()` as a unary operator 

