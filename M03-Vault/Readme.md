# Getting start with HashiCrop Vault
### Start Vault server in dev mode
```
vault server -dev
$env:VAULT_ADDR="http://127.0.0.1:8200"
vault login
```

### Authentication command
```
vault auth list
vault auth enable userpass
vault write auth/userpass/users/demo password=demo
vault auth disable userpass
vault login --method=userpass username=demo
```

### Working with secret engine
```
vault secrets list
vault secrets enable -path=mysecret kv
vault secrets enable -path=secretv2 kv-v2
vault secrets tune -description="describe your path here" mysecret
vault secrets move mysecret mysecret-v1
vault secrets disable mysecret-v1
```

### Writing secret value
```
vault kv put secretv2/apikeys/demo token=0123456789
vault kv list secretv2/apikeys
vault kv get secretv2/apikeys/demo
vault kv get -version=1 secretv2/apikeys/demo
vault kv delete -version=1 secretv2/apikeys/demo
vault kv destroy -version=1 secretv2/apikeys/demo
vault secret enable mysecret kv
vault kv enable-versioning mysecret
vault kv rollback -version=1 secret/apikey
vault kv patch secret/apikey token2=asdgqwer token3=asdfghjkwert
```

### Working with token
```
vault token create
vault token create -ttl=5m
vault token renew -increment=5m
vault token lookup [token]
vault token lookup -accessor [accessor]
vault token capabilities [token] mysecret/apikeys
vault token revoke [token]
vault token revoke -accessor [accessor]
```

### Working with policy
```
vault policy list
vault policy write dev-policy dev-policy.hcl
vault policy read dev-policy
vault policy delete dev-policy
vault token create -policy=dev-policy
vault write auth/userpass/users/demo token_policy="dev-policy"
vault write identity/entity/name/demo policies="dev-policy"
```
