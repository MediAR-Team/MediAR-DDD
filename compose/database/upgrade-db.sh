for i in {1..50};
do
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P SqlServer2202Pass -d master -i create-db.sql
    if [ $? -eq 0 ]
    then
        break
    else
        sleep 1
    fi
done

dotnet Migrator/DatabaseMigrator.dll "Server = localhost; Initial Catalog = MediAR_DDD; User Id = sa; Password = SqlServer2202Pass;" "Scripts"
/bin/sh
