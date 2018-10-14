"use strict"
let target = process.argv[2],
    path = process.argv[3],
    type = process.argv[4] || "file";

require("fs").symlinkSync(target, path, type);