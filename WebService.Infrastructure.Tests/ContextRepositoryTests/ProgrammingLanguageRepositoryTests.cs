﻿using System.Diagnostics.CodeAnalysis;

namespace WebService.Infrastructure.Tests.ContextRepositoryTests;

[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class ProgrammingProgrammingLanguageRepositoryTests
{
    private readonly TestVariables _v;

    public ProgrammingProgrammingLanguageRepositoryTests()
    {
        _v = new TestVariables();
    }

    #region Create

    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_new_programmingLanguage_with_id()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("Java");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.Created, new ProgrammingLanguageDTO(4, "Java"));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_conflict_and_existing_programmingLanguage()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("F#");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.Conflict, new ProgrammingLanguageDTO(3, "F#"));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_count_one_more()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("Java");

        await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var actual = _v.ProgrammingLanguageRepository.ReadAsync().Result.Count;

        const int expected = 4;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_bad_request_on_name_tooLong()
    {
        var programmingLanguage =
            new CreateProgrammingLanguageDTO("asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.BadRequest,
            new ProgrammingLanguageDTO(-1, "asseocarnisanguineoviscericartilaginonervomedullary"));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_bad_request_on_name_empty()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO("");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.BadRequest, new ProgrammingLanguageDTO(-1, ""));

        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task CreateAsync_programmingLanguage_returns_bad_request_on_name_whitespace()
    {
        var programmingLanguage = new CreateProgrammingLanguageDTO(" ");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.BadRequest, new ProgrammingLanguageDTO(-1, " "));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task CreateAsync_programmingLanguage_with_max_length_returns_new_language_with_id()
    {
        var programmingLanguage =
            new CreateProgrammingLanguageDTO("asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.ProgrammingLanguageRepository.CreateAsync(programmingLanguage);

        var expected = (Status.Created,
            new ProgrammingLanguageDTO(4, "asseocarnisanguineoviscericartilaginonervomedullar"));

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Read

    [Fact]
    public async Task ReadAsync_programmingLanguage_by_id_returns_programmingLanguage_and_status_found()
    {
        var actual = await _v.ProgrammingLanguageRepository.ReadAsync(1);

        var expected = (Status.Found, new ProgrammingLanguageDTO(1, "C#"));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAsync_programmingLanguage_by_id_returns_empty_programmingLanguage_and_status_notFound()
    {
        var actual = await _v.ProgrammingLanguageRepository.ReadAsync(4);

        var expected = (Status.NotFound, new ProgrammingLanguageDTO(-1, ""));

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task ReadAllAsync_returns_all_programmingLanguages()
    {
        var actual = await _v.ProgrammingLanguageRepository.ReadAsync();

        var expected1 = new ProgrammingLanguageDTO(1, "C#");
        var expected2 = new ProgrammingLanguageDTO(2, "C++");
        var expected3 = new ProgrammingLanguageDTO(3, "F#");

        Assert.Collection(actual,
            programmingLanguageDTO => Assert.Equal(expected1, programmingLanguageDTO),
            programmingLanguageDTO => Assert.Equal(expected2, programmingLanguageDTO),
            programmingLanguageDTO => Assert.Equal(expected3, programmingLanguageDTO));
    }

    #endregion

    #region Delete

    [Fact]
    public async Task DeleteAsync_programmingLanguage_by_id_returns_status_deleted()
    {
        var actual = await _v.ProgrammingLanguageRepository.DeleteAsync(1);

        const Status expected = Status.Deleted;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAsync_programmingLanguage_by_id_returns_status_notFound()
    {
        var actual = await _v.ProgrammingLanguageRepository.DeleteAsync(4);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteAsync_programmingLanguage_by_id_returns_count_one_less()
    {
        await _v.ProgrammingLanguageRepository.DeleteAsync(3);

        var actual = _v.ProgrammingLanguageRepository.ReadAsync().Result.Count;

        const int expected = 2;

        Assert.Equal(expected, actual);
    }

    #endregion

    #region Update

    [Fact]
    public async Task UpdateAsync_programmingLanguage_by_id_returns_status_updated()
    {
        var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "Java");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task
        UpdateAsync_programmingLanguage_by_id_read_updated_returns_status_found_and_updated_programmingLanguage()
    {
        var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "Java");

        await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

        var actual = await _v.ProgrammingLanguageRepository.ReadAsync(3);

        var expected = (Status.Found, updateProgrammingLanguageDTO);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_programmingLanguage_by_id_returns_status_notFound()
    {
        var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(4, "Java");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

        const Status expected = Status.NotFound;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_programmingLanguage_by_id_returns_status_conflict()
    {
        var updateProgrammingLanguageDTO = new ProgrammingLanguageDTO(3, "C#");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(updateProgrammingLanguageDTO);

        const Status expected = Status.Conflict;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_programmingLanguage_returns_bad_request_on_name_tooLong()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "asseocarnisanguineoviscericartilaginonervomedullary");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(programmingLanguage);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_programmingLanguage_returns_bad_request_on_name_empty()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(programmingLanguage);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }


    [Fact]
    public async Task UpdateAsync_programmingLanguage_returns_bad_request_on_name_whitespace()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, " ");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(programmingLanguage);

        const Status expected = Status.BadRequest;

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task UpdateAsync_programmingLanguage_with_max_length_returns_updated()
    {
        var programmingLanguage = new ProgrammingLanguageDTO(1, "asseocarnisanguineoviscericartilaginonervomedullar");

        var actual = await _v.ProgrammingLanguageRepository.UpdateAsync(programmingLanguage);

        const Status expected = Status.Updated;

        Assert.Equal(expected, actual);
    }

    #endregion
}