path "secret-kv/*" {
    capabilities=["create", "read", "update", "delete", "list"]
}

path "sys/*" {
    capabilities=["create", "read", "update", "delete", "list"]
}
