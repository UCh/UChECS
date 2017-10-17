namespace uchlab.benchmark {
    public enum LogEntry {
        Total,
        RepoContainKey,
        Iterating,
        CreateComponentSet,
        EntityAddComponent,
        ForEachAddIfValid,
        AddIfValid_InnerIf,
        AddIfValid_addListeners,
        AddIfValid_addLast,
        AddIfValid_dispatchNodeAdded,
        DestroyAll
    }
}