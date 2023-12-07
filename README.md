# Unity Code Conventions

## Introduction
This document outlines the coding conventions to be followed when writing Unity scripts. Adhering to these conventions ensures consistency and readability across the project.

## Variable Naming
**Capitalization:** Variable names should start with a capital letter.

```
// Good
int PlayerScore;

// Avoid
int playerScore;
```
**Descriptive Names:** Choose meaningful names that convey the purpose of the variable.

```
// Good
string playerName;

// Avoid
string pName;
```
## Variable
**Private Variables:** Private variables are declared at the top of the script.

```
public class ExampleScript : MonoBehaviour
{
    private int health = 100;
    private string playerName = "Player1";

    // ... rest of the code
}
```
**Public Variables:** Public variables are declared after private variables.

```
public class ExampleScript : MonoBehaviour
{
    private int health = 100;
    private string playerName = "Player1";

    public float movementSpeed = 5f;
    public int playerLevel = 1;

    // ... rest of the code
}
```
**Private Variables When Possible:** Declare variables as private if they don't need to be accessed outside the script.

```
private int internalCounter = 0;
```
## Functions
**Position:** If your script contains both an 'Update' function and a 'start' function, place the 'start' function at the top of the script and the 'Update' function underneath for easy visibility.

```
public class ExampleScript : MonoBehaviour
{
    void Start()
    {
        // Start logic
    }

    void Update()
    {
        // Update logic
    }

    // ... rest of the code
}
```

## DRY (Don't Repeat Yourself)
**Avoid** code duplication by encapsulating repetitive logic in functions or methods. Reusable components or utility classes should be created to promote maintainability and readability.
```
// Example of avoiding repetition using a function
void Start()
{
    InitializeGame();
}

void InitializeGame()
{
    // Common initialization logic
}
```
## SRP (Single Responsibility Principle)
Adhere to the Single Responsibility Principle by ensuring that each class or function has a clear and specific responsibility. Avoid bundling unrelated functionalities within a single script.

```
// Good example: Each class has a single responsibility
public class Player : MonoBehaviour
{
    // Player-related logic
}

public class ScoreManager : MonoBehaviour
{
    // Score management logic
}
```
## Inheritance
Use inheritance judiciously for code reuse and extensibility. Ensure that the base class provides a common interface or functionality shared by its derived classes.

```
// Base class with common functionality
public class BaseEnemy : MonoBehaviour
{
    protected int health;
    // Common enemy logic
}

// Derived class inheriting from BaseEnemy
public class BossEnemy : BaseEnemy
{
    // Additional logic specific to the boss enemy
}
```
## Events
Events are used to facilitate communication between scripts. Declare events using the event keyword and follow a consistent naming convention.

```
public class EventExample : MonoBehaviour
{
    public delegate void MyEventHandler();
    public event MyEventHandler OnMyEvent;

    void Start()
    {
        // Trigger the event
        OnMyEvent?.Invoke();
    }
}
```
## Serialized Fields
Use the [SerializeField] attribute to expose private fields in the Unity Inspector. This allows for easy tweaking of variables without compromising encapsulation.

```
public class ExampleScript : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private string playerName = "Player1";

    // ... rest of the code
}
```