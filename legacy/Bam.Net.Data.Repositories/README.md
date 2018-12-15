## DaoRepository Notes

 - Automatic wiring of one-to-one relationships is not supported, either create an 
implicit one-to-many and enforce it as one-to-one in code or manually wire the 
relationship in code

- Query will not fully hydrate child lists, to fully hydrate call Retrieve(id) or 
Retrieve(uuid)

- Setting foreign key Id will hydrate related property on Retrieve

- If a class defines a parent class (ParentId and Parent property pairs) and the parent 
doesn't define a child array no association will be implicitly made

- If a class defines a parent class (ParentId and Parent property pairs) and the childs 
property representing the parent is not declared "virtual" no association will be 
implicitly made

- If a class defines a parent class (ParentId and Parent property pairs) the child must
follow the naming convention shown here for implicit association:

```
	public long [ParentTypeName]Id { get; set; }
	public virtual [ParentTypeName] [ParentTypeName] { get; set; } 
```

- If a class defines a child list and the child list is not declared "virtual" no association
will be implicitly made