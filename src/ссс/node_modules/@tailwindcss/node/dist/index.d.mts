declare function compile(css: string, { base, onDependency }: {
    base: string;
    onDependency: (path: string) => void;
}): Promise<{
    globs: {
        origin?: string;
        pattern: string;
    }[];
    build(candidates: string[]): string;
}>;

export { compile };
