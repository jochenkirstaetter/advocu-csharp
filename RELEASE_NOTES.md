# Release Notes 0.2.0

## 🌟 Interactive Mode & Token persistence

This release introduces a major usability improvement for the `advocu` CLI!

### 🧙‍♂️ Interactive Wizard
Gone are the days of remembering long lists of CLI arguments. Simply run:
```bash
advocu interactive
```
And the CLI will guide you through the process of creating your activity draft, complete with:
- **Rich Selection Menus**: Easily pick activity types, countries, and tags.
- **Persistent Drafts**: Started a draft but need to check a link? No problem! Exit the tool and your draft is saved automatically. Resume right where you left off.

### 🔑 Smart Token Management
You no longer need to set environment variables manually every time (though you still can!).
- **Auto-Prompt**: If the CLI can't find your token, it will securely ask for it once.
- **Persisted**: Your token is saved securely in your user profile, so you only need to enter it once.

### 🐛 Improvements
- Improved validation logic.
- Standardized token resolution across all commands.

Happy Reporting! 🚀
