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

## Features

* Very modular, operators are added at runtime by instanciating any IOperator class
* Use any converter or parser you want, as long as it extends the interfaces. 
