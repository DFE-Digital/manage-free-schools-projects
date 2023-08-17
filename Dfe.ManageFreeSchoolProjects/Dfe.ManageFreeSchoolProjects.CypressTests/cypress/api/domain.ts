export type CsvTable<TRow> = {
    headers: Array<string>;
    rows: Array<TRow>;
};

export type ProjectTableRow = {
    projectId?: string;
    projectTitle?: string;
    trustName?: string;
    region?: string;
    localAuthority?: string;
    realisticOpeningDate?: string;
    status?: string;
};
