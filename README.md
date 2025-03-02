Singleton
===

## Description
This package contains a utility to ensure that a class has only one instance and is easily accessible from anywhere and that instance doesn't destroy when you switch between the scenes.

## How to use
Just copy the link below and add it to your project via Unity Package Manager: [Installing from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
```
https://github.com/sajadimn/noname-oneup-singleton.git
```

## Problem
The Singleton pattern solves three problems at the same time, violating the Single Responsibility Principle:

### 1-Ensure that a class has just a single instance.
Why would anyone want to control how many instances a class has? The most common reason for this is to control access to some shared resource—for example, a database or a file.

### 2-Provide a global access point to that instance.
Remember those global variables that you used to store some essential objects? While they’re very handy, they’re also very unsafe since any code can potentially overwrite the contents of those variables and crash the app. Just like a global variable, the Singleton pattern lets you access some object from anywhere in the program. However, it also protects that instance from being overwritten by other code.

### 3-Ensure that an instance(class or object) doesn't destroy when you switch between the scenes.
Sometimes, you need to keep the instance alive until you exit from the application.

## Examples

✅️ Inherit from this base class to create a MonoSingleton
```c#
  public class MyClassName : MonoSingleton<MyClassName> {
    
    protected override void OnCreateSingleton()
    {
    }

    protected override void OnDestroySingleton()
    {
    }
  }
```

✅️ Inherit from this base class to create a Singleton
```c#
  public class MyClassName : Singleton<MyClassName> {
    
    protected override void OnCreateSingleton()
    {
    }

    protected override void OnDestroySingleton()
    {
    }
  }
```
## License

* MIT
* [Singleton](https://github.com/sajadimn/noname-oneup-singleton) by Sajad Imani

## Author

[Sajad Imani](https://github.com/sajadimn)

## See Also

* GitHub Page : https://github.com/sajadimn
