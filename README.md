Cecil.Samples
=============

This repository contains samples demonstrating how to use [Cecil](https://github.com/jbevain/cecil).

For this project to build, you need to clone Cecil along with it.

All samples are NUnit tests in disguise, and can be run from Visual Studio provided you have a NUnit Test Runner or ReSharper.

A sample is made of two projects: one Target project which will be the subject of the analyze or the modification, and a Sample project containing the code using Cecil manipulating the Target project.

This repository is a work in progress and will be updated with new samples to demonstrate how to use Cecil based on commonly asked questions.

### Samples

#### [AddAttributeToMethod](https://github.com/jbevain/cecil.samples/tree/master/Samples/AddAttributeToMethod)

This samples shows how to add a CustomAttribute to a method.

#### [ReplaceMethodCall](https://github.com/jbevain/cecil.samples/tree/master/Samples/ReplaceMethodCall)

This samples shows how to modify the IL code of a method to to replace a method call by another one another..

