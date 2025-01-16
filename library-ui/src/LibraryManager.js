import React, { useEffect, useState } from "react";
import axios from "axios";
import "./LibraryManager.css"; // Import the CSS file

function LibraryManager() {
  const [books, setBooks] = useState([]);
  const [filteredBooks, setFilteredBooks] = useState([]); // Filtrelenmiş kitaplar
  const [searchQuery, setSearchQuery] = useState(""); // Arama sorgusu
  const [selectedCategory, setSelectedCategory] = useState(""); // Seçilen kategori
  const [authors, setAuthors] = useState([]);
  const [categories, setCategories] = useState([]);
  const [members, setMembers] = useState([]);
  const [newBook, setNewBook] = useState({ title: "", authorId: "", categoryId: "" });
  const [newAuthor, setNewAuthor] = useState({ name: "", surname: "" });
  const [newCategory, setNewCategory] = useState({ name: "" });
  const [newMember, setNewMember] = useState({ name: "", surname: "" });
  const [loanInfo, setLoanInfo] = useState({ bookId: "", memberId: "", loanDate: "" });
  const [selectedBookForLoan, setSelectedBookForLoan] = useState(null);
  const [loanedBooks, setLoanedBooks] = useState([]);

  useEffect(() => {
    fetchBooks();
    fetchAuthors();
    fetchCategories();
    fetchMembers();
    fetchLoanedBooks();
  }, []);

  const fetchBooks = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Books");
      const booksData = response.data?.$values ?? [];
      setBooks(booksData);
      setFilteredBooks(booksData); // Başlangıçta tüm kitaplar gösterilir
    } catch (error) {
      console.error("Error fetching books", error);
    }
  };

  const handleSearch = () => {
    const query = searchQuery.toLowerCase();
    const results = books.filter((book) => book.title.toLowerCase().includes(query));
    setFilteredBooks(results);
  };

  const fetchAuthors = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Authors");
      const authorsData = response.data?.$values ?? [];
      setAuthors(authorsData);
    } catch (error) {
      console.error("Error fetching authors", error);
    }
  };

  const fetchCategories = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Categories");
      const categoriesData = response.data?.$values ?? [];
      setCategories(categoriesData);
    } catch (error) {
      console.error("Error fetching categories", error);
    }
  };

  const fetchMembers = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Members");
      const membersData = response.data?.$values ?? [];
      setMembers(membersData);
    } catch (error) {
      console.error("Error fetching members", error);
    }
  };

  const fetchLoanedBooks = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Loans");
      const loanedData = response.data?.$values ?? [];
      setLoanedBooks(loanedData);
    } catch (error) {
      console.error("Error fetching loaned books", error);
    }
  };

  const handleFilterByCategory = () => {
    if (selectedCategory) {
      const filtered = books.filter((book) => book.categoryId === parseInt(selectedCategory));
      setFilteredBooks(filtered);
    } else {
      setFilteredBooks(books); // Kategori seçilmezse tüm kitapları göster
    }
  };

  const addBook = async () => {
    try {
      const response = await axios.post("http://localhost:5098/api/Books", newBook);
      if (response.status === 201) {
        fetchBooks();
      }
    } catch (error) {
      console.error("Error adding book", error);
    }
  };

  const addAuthor = async () => {
    try {
      const response = await axios.post("http://localhost:5098/api/Authors", newAuthor);
      if (response.status === 201) {
        fetchAuthors();
      }
    } catch (error) {
      console.error("Error adding author", error);
    }
  };

  const addCategory = async () => {
    try {
      const response = await axios.post("http://localhost:5098/api/Categories", newCategory);
      if (response.status === 201) {
        fetchCategories();
      }
    } catch (error) {
      console.error("Error adding category", error);
    }
  };

  const addMember = async () => {
    try {
      const response = await axios.post("http://localhost:5098/api/Members", newMember);
      if (response.status === 201) {
        fetchMembers();
      }
    } catch (error) {
      console.error("Error adding member", error);
    }
  };

  const loanBook = async () => {
    try {
      if (!loanInfo.memberId || !loanInfo.returnDate) {
        alert("Please select a member and a return date.");
        return;
      }
  
      const loanData = {
        bookId: selectedBookForLoan,
        memberId: loanInfo.memberId,
        returnDate: new Date(loanInfo.returnDate).toISOString(), // Kullanıcıdan alınan ReturnDate'i UTC'ye dönüştürme
      };
  
      const response = await axios.post("http://localhost:5098/api/Loans", loanData);
      if (response.status === 201) {
        alert("Book loaned successfully");
        fetchLoanedBooks(); // Ödünç verilmiş kitapları tekrar çek
        setSelectedBookForLoan(null);
        setLoanInfo({ bookId: "", memberId: "", returnDate: "" });
      }
    } catch (error) {
      console.error("Error loaning book", error);
    }
  };
  
  
  

  return (
    <div className="library-manager">
      <h1>Library Management</h1>
      
      {/* Kitap Ekleme Bölümü */}
      <div className="section">
        <h2>Add New Book</h2>
        <input
          type="text"
          placeholder="Title"
          value={newBook.title}
          onChange={(e) => setNewBook({ ...newBook, title: e.target.value })}
        />
        <select
          value={newBook.authorId}
          onChange={(e) => setNewBook({ ...newBook, authorId: e.target.value })}
        >
          <option value="">Select Author</option>
          {authors.map((author) => (
            <option key={author.id} value={author.id}>
              {author.name} {author.surname}
            </option>
          ))}
        </select>
        <select
          value={newBook.categoryId}
          onChange={(e) => setNewBook({ ...newBook, categoryId: e.target.value })}
        >
          <option value="">Select Category</option>
          {categories.map((category) => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
        <button onClick={addBook}>Add Book</button>
      </div>

      {/* Yazar Ekleme Bölümü */}
      <div className="section">
        <h2>Add New Author</h2>
        <input
          type="text"
          placeholder="Name"
          value={newAuthor.name}
          onChange={(e) => setNewAuthor({ ...newAuthor, name: e.target.value })}
        />
        <input
          type="text"
          placeholder="Surname"
          value={newAuthor.surname}
          onChange={(e) => setNewAuthor({ ...newAuthor, surname: e.target.value })}
        />
        <button onClick={addAuthor}>Add Author</button>
      </div>

      {/* Kategori Ekleme Bölümü */}
      <div className="section">
        <h2>Add New Category</h2>
        <input
          type="text"
          placeholder="Name"
          value={newCategory.name}
          onChange={(e) => setNewCategory({ ...newCategory, name: e.target.value })}
        />
        <button onClick={addCategory}>Add Category</button>
      </div>

      {/* Üye Ekleme Bölümü */}
      <div className="section">
        <h2>Add New Member</h2>
        <input
          type="text"
          placeholder="Name"
          value={newMember.name}
          onChange={(e) => setNewMember({ ...newMember, name: e.target.value })}
        />
        <input
          type="text"
          placeholder="Surname"
          value={newMember.surname}
          onChange={(e) => setNewMember({ ...newMember, surname: e.target.value })}
        />
        <button onClick={addMember}>Add Member</button>
      </div>
{/* Kitap Arama ve Filtreleme Bölümü */}
<div className="section">
        <h2>Search and Filter Books</h2>
        <input
          type="text"
          placeholder="Enter book title"
          value={searchQuery}
          onChange={(e) => setSearchQuery(e.target.value)}
        />
        <button onClick={handleSearch}>Search</button>
        <select
          value={selectedCategory}
          onChange={(e) => setSelectedCategory(e.target.value)}
        >
          <option value="">All Categories</option>
          {categories.map((category) => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
        <button onClick={handleFilterByCategory}>Filter by Category</button>
      </div>

      {/* Kitap Listeleme ve Loan Bölümü */}
      <div className="section">
        <h2>Book List</h2>
        <ul>
          {filteredBooks.length > 0 ? (
            filteredBooks.map((book) => (
              <li key={book.id}>
                {book.title} by {book.authorName} (Category: {book.categoryName})
                <button
                  onClick={() => {
                    setSelectedBookForLoan(book.id);
                    setLoanInfo({ ...loanInfo, bookId: book.id });
                  }}
                >
                  Loan Book
                </button>
                {selectedBookForLoan === book.id && (
                  <div>
                    <select
                      value={loanInfo.memberId}
                      onChange={(e) => setLoanInfo({ ...loanInfo, memberId: e.target.value })}
                    >
                      <option value="">Select Member</option>
                      {members.map((member) => (
                        <option key={member.id} value={member.id}>
                          {member.name} {member.surname}
                        </option>
                      ))}
                    </select>
                    <input
                      type="date"
                      value={loanInfo.returnDate}
                      onChange={(e) => setLoanInfo({ ...loanInfo, returnDate: e.target.value })}
                    />
                    <button onClick={loanBook}>Confirm Loan</button>
                  </div>
                )}
              </li>
            ))
          ) : (
            <p>No books found</p>
          )}
        </ul>
      </div>



      {/* Ödünç Verilmiş Kitaplar Bölümü */}
<div className="section">
  <h2>Loaned Books</h2>
  <ul>
    {loanedBooks.length > 0 ? (
      loanedBooks.map((loan) => {
        const book = books.find((b) => b.id === loan.bookId);
        const member = members.find((m) => m.id === loan.memberId);
        const formattedLoanDate = new Date(loan.loanDate).toLocaleDateString();
        
        return (
          <li key={loan.id}>
            {book?.title || "Unknown Book"} loaned to {member?.name || "Unknown Member"}{" "}
            {member?.surname ? member.surname : ""} until {formattedLoanDate}
          </li>
        );
      })
    ) : (
      <p>No loaned books available</p>
    )}
  </ul>
</div>


      {/* Yazar Listesi Bölümü */}
      <div className="section">
        <h2>Author List</h2>
        <ul>
          {authors.length > 0 ? (
            authors.map((author) => (
              <li key={author.id}>
                {author.name} {author.surname}
              </li>
            ))
          ) : (
            <p>No authors available</p>
          )}
        </ul>
      </div>

      {/* Kategori Listesi Bölümü */}
      <div className="section">
        <h2>Category List</h2>
        <ul>
          {categories.length > 0 ? (
            categories.map((category) => (
              <li key={category.id}>{category.name}</li>
            ))
          ) : (
            <p>No categories available</p>
          )}
        </ul>
      </div>
    </div>
  );
}

export default LibraryManager;
