# Clean Architecture
![alt text](https://github.com/khoanguyen012/Clean-Architecture/blob/master/docs/imgs/Clean-Architecture.png)

# Solution Structure
![alt text](https://github.com/khoanguyen012/Clean-Architecture/blob/master/docs/imgs/Solution-Structure.png)

# How to run

## Update connection string
| Project             | Configuration File  | Configuration Key  |
| -------------       | -------------       | --------           |
| CA.WebAPI           | appsettings.json    | DefaultConnection  |
| CA.BackgroundServer | appsettings.json    | DefaultConnection  |

## Migration
- Set `CA.WebAPI` as StartUp Project
- Open Package Manager Console, select `CA.Infrastructure` as Default Project
- Run these commands:
  - add-migration `migration name`
  - update-database

