import React, { useEffect, useState } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

function BookList() {
  const [books, setBooks] = useState([]);
  const [query, setQuery] = useState("");
  const [newBook, setNewBook] = useState({ title: "", authorId: "", categoryId: "" });
  const [loanBookId, setLoanBookId] = useState("");
  const [members, setMembers] = useState([]);
  const [selectedBookForLoan, setSelectedBookForLoan] = useState(null);
  const [loanInfo, setLoanInfo] = useState({ bookId: "", memberId: "", returnDate: "" });

  useEffect(() => {
    fetchBooks();
    fetchMembers();
  }, []);

  const fetchBooks = async () => {
    try {
      const response = await axios.get("http://localhost:5098/api/Books");
      // API yanıtında $values varsa onu kullanıyoruz
      const booksData = response.data?.$values ?? [];
      setBooks(booksData);
    } catch (error) {
      console.error("Error fetching books", error);
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

  const searchBooks = async () => {
    try {
      const response = await axios.get(`http://localhost:5098/api/books/search?query=${query}`);
      setBooks(response.data);
    } catch (error) {
      console.error("Error searching books", error);
    }
  };

  const addBook = async () => {
    try {
      const response = await axios.post("http://localhost:5098/api/books", newBook);
      if (response.status === 201) {
        fetchBooks(); // Kitap eklendikten sonra listeyi güncelle
      }
    } catch (error) {
      console.error("Error adding book", error);
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
        fetchBooks(); // Ödünç verilmiş kitapları tekrar çek
        setSelectedBookForLoan(null);
        setLoanInfo({ bookId: "", memberId: "", returnDate: "" });
      }
    } catch (error) {
      console.error("Error loaning book", error);
    }
  };

  return (
    <div>
      <h1>Library Book List</h1>
      <nav>
        <Link to="/add-book">Add Book</Link> | <Link to="/loan-book">Loan Book</Link>
      </nav>
      <div>
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Search by title"
        />
        <button onClick={searchBooks}>Search</button>
      </div>
      <div>
        <h2>Add New Book</h2>
        <input
          type="text"
          placeholder="Title"
          value={newBook.title}
          onChange={(e) => setNewBook({ ...newBook, title: e.target.value })}
        />
        <input
          type="text"
          placeholder="Author ID"
          value={newBook.authorId}
          onChange={(e) => setNewBook({ ...newBook, authorId: e.target.value })}
        />
        <input
          type="text"
          placeholder="Category ID"
          value={newBook.categoryId}
          onChange={(e) => setNewBook({ ...newBook, categoryId: e.target.value })}
        />
        <button onClick={addBook}>Add Book</button>
      </div>
      <ul>
  {Array.isArray(books) && books.length > 0 ? (
    books.map((book) => (
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
    <p>No books available</p>
  )}
</ul>

    </div>
  );
}

export default BookList;