## Unity Code Conventions

# Introduction
This document outlines the coding conventions to be followed when writing Unity scripts. Adhering to these conventions ensures consistency and readability across the project.

# Variable Naming
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
# Variable Placement
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
# Update Function
**Position:** If your script contains an Update function, place it at the top of the script for easy visibility.

```
public class ExampleScript : MonoBehaviour
{
    void Update()
    {
        // Update logic
    }

    // ... rest of the code
}
```