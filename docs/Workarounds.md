## Tenant management workaround
Default tenant has origin `default.mediar.app`, so for the angular app to work, add to the `hosts` file the following:
```
127.0.0.1 default.mediar.app
```
and when launching angular app, use
```
ng s --disableHostChecking
```
and go to address `http://default.mediar.app:4200`.
## Minio workaround
If using project from `docker-compose`, API returns minio links with host `nginx_minio`. Workaround is adding to the `hosts` file:
```
127.0.0.1 nginx_minio
```
