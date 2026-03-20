#!/bin/bash
# Remove all bin and obj folders recursively

echo "Cleaning up bin and obj folders..."
find . -type d \( -name "bin" -o -name "obj" \) -prune -exec rm -rf {} +
echo "Cleanup completed successfully!"
