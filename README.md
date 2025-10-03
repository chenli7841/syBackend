# EplusCore

This repo is for Eplus International Inc. 

It is only intended for Admin to use.

The architecture follows clean architecture pattern and is inspired by https://github.com/jasontaylordev/CleanArchitecture. 

## Useful info:
1. DB-First approach is used.
2. How to scaffold model from MySql db table: 
    1. run the following in Persistence project (replace the CONNECTION_STRING_PLACE_HOLDER with actual connection string)
    ```
    Scaffold-DbContext "CONNECTION_STRING_PLACE_HOLDER;TreatTinyAsBoolean=true;" "Pomelo.EntityFrameworkCore.MySql" -OutputDir Data -f -Context EplusDbContext
    ```
    2. Inside Persistence project, replace `sbyte` as `bool`
3. UI Template can be found in the UITemplate folder

