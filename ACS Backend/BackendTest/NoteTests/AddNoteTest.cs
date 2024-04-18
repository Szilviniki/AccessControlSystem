namespace BackendTest.NoteTests;

[TestClass]
public class AddNoteTest
{
    private static SQL _sql = new();
    private StudentService _studentService = new(new SQL());
    private Student _student = new MockStudent().Student;
    private Note note = new MockNote().Note;
    private static Guardian _guardian = new MockGuardian().Parent;

    [TestInitialize]
    public async Task Setup()
    {
        if (!_sql.Guardians.Any(x => x.Id == _guardian.Id))
        {
            _sql.Guardians.AddAsync(_guardian);
            await _sql.SaveChangesAsync();
        }

        if (!_sql.Students.Any(x => x.Email == _student.Email))
        {
            _student.ParentId = _guardian.Id;
            _sql.Students.Add(_student);
        }

        await _sql.SaveChangesAsync();
    }
    
    [TestCleanup]
    public async Task Cleanup()
    {
        if (_sql.Notes.Any(x => x.Id == note.Id))
        {
            _sql.Notes.Remove(await _sql.Notes.FindAsync(note.Id));
            await _sql.SaveChangesAsync();
        }

        if (_sql.Students.Any(x => x.Id == _student.Id))
        {
            _sql.Students.Remove(_student);
            await _sql.SaveChangesAsync();
        }
    }
    
    [TestMethod]
    public async Task AddNoteToStudent()
    {
        try
        {
            await _studentService.AddNoteToStudent(note);
        }
        catch (Exception e)
        {
            Assert.Fail(e.Message);
        }
    }
    
    [TestMethod]
    public async Task AddNoteToStudentBadStudentId()
    {
        try
        {
            var bad = new MockNote().DeepCopy();
            bad.StudentId = Guid.NewGuid();
            await _studentService.AddNoteToStudent(bad);
            Assert.Fail();
        }
        catch (ItemNotFoundException e)
        {
        }
    }
    
    [TestMethod]
    public async Task AddNoteToStudentBadNote()
    {
        try
        {
            var bad = new MockNote().DeepCopy();
            bad.Name = null;
            await _studentService.AddNoteToStudent(bad);
            Assert.Fail();
        }
        catch (ArgumentException e)
        {
        }
    }

    [TestMethod]
    public async Task AddNoteEmptyStudentId()
    {
        try
        {
            var bad = new MockNote().DeepCopy();
            bad.StudentId = Guid.Empty;
            await _studentService.AddNoteToStudent(bad);
            Assert.Fail();
        }
        catch (ItemNotFoundException)
        {
            
        }
    }
}