## Update connection string
| Project             | Configuration File  | Configuration Key  |
| -------------       | -------------       | --------           |
| Utils.WebAPI           | appsettings.json    | DefaultConnection  |
| Utils.BackgroundServer | appsettings.json    | DefaultConnection  |

## Migration
- Set `Utils.WebAPI` as StartUp Project
- Open Package Manager Console, select `CA.Infrastructure` as Default Project
- Run these commands:
  - add-migration `migration name`
  - update-database

