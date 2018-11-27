# Introduction 
DKInterpreter is an interpretation tool allowing the interpretation of one string to another, by providing a list of rules
under which this will occur.

# Getting Started
DKInterpreter is a library build in .NET 4.6.1 and C# 7.

## Installation process

## Software dependencies
It uses only the buil-in .NET core libraries.

## Latest releases

## API references

# Build and Test

# Operation description
The DKInterpreter is separated into two processes:
1. The lexing process under which the input string is broken into ```IToken```s.
1. The parsing process under which the collection of ```IToken```s is turned into a new string.

These processes take the form of an ```ILexer``` and an ```IParser``` and are separated from one another, 
allowing the interpolation of a third process to possibly manipulate the ```IToken```s.

## The Lexor
The ```Lexor``` class will break the input string into a collection of ```IToken```s, according to a set 
of ```ITokenType```s provided. If no ```ITokenType```s are provided, the result will be a separate ```IToken```
for each character in the input.

## The Parser
The ```Parser``` class will take a collection of ```IToken```s and turn them into a string, according to a set
of ```IRule```s provided. If no ```IRule``` are provided, the result will be a concatenation of the ```IToken```s'
values. 

# How to use it
## 1. Create the ```ITokenType``` classes
Create a token type classs, implementing the ```ITokenType``` interface, for each type of ```IToken```s the lexing process
should break the input string to. It is in the responsibility of each token type class to decide how to break the string.

Note that the Lexer will loop through every ```ITokenType``` provided, _in the order that they are provided_. 
So if two ```ITokenType```s can be satisfied by a char, only the first one will count. Make sure that you provide the 
collection of ```ITokenType```s in the correct order.

### how to write a ```ITokenType``` class
A ```IsSatisfied``` method receives the whole string to be interpreted and an index for the current location on the string.
It should make all relevant checks on the current character and consider all possible dependencies on the rest of the string
to determine if the token type is satisfied.

If satisfied, the ```Lexer``` will create a new ```IToken``` with the specified ```ITokenType``` and the text of the token will be the ```ITokenType.TextSatisfied```. The ```Lexer``` will then move the location of the cursor as many characters as the ```ITokenType.TextSatisfied``` uses.

## 2. Create the ```IRule``` classes
Create a rule class, implementing the ```IRule``` interface, for each individual interpretation of an ```IToken``` 
the parsing process should make. It is in the responsibility of each rule class to decide how the ```IToken```s will be
interpreted. 

### how to write a ```IRule``` class
A ```IsSatisfied``` method receives the whole collection of ```IToken```s to be interpreted and an index for the current location on the collection. It should make all relevant checks on the current ```IToken``` and consider all possible dependencies on the rest of the collection to determine if the rule is satisfied.

If satisfied, the ```Parser``` will add to the interpretation the ```IRule.Value```.

## 3. Create the ```IInterpreter``` class
Create a new class for the interpreter implementing the ```IInterpreter``` interface. 
You can use the ```Lexer``` and ```Parser``` classes provided or implement your own. This interpreter class should provide 
the ```ILexer``` and the ```IParser``` with the set of ```ITokenType```s and ```IRule```s in order to make the lexing and the 
parsing accordingly.

# Contribute
Before pushing any changes, please make sure to update the ```PackageVersion``` property of the ```DKInterpreter.csproj``` file,
according to the **Semantic Versioning convention** (SemVer 1.0).

Visit this [page](https://docs.microsoft.com/en-us/nuget/reference/package-versioning) for guidance. 
