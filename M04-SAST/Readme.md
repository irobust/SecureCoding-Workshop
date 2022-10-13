# Static Analysis Security Testing
### Linting Source Code with Sonar Lint
1. Install Extension on VS Code
1. Set JAVA_HOME and Node executable path
1. Test code with no quality

```
function foo(a) { 
  let b = 12;
  if (a) {
    return b;
  }
  return b;
}
```

4. Test Code with Vulnerability
```
document.location = document.location.hash.slice(1);
```

### Check Vulnerable Component with OWASP Dependency Check
1. pull source code from https://github.com/irobust/dependencyCheckDemo
1. Install OWASP Dependency Check
