using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistryResources.Data.Migrations
{
    public partial class businessobjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street1 = table.Column<string>(nullable: true),
                    Street2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Latitide = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    BadAddress = table.Column<bool>(nullable: false),
                    NoMail = table.Column<bool>(nullable: false),
                    NeedsVerification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(nullable: false),
                    Answer = table.Column<string>(nullable: true),
                    AnswerDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                });

            migrationBuilder.CreateTable(
                name: "Cultures",
                columns: table => new
                {
                    CultureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Culture = table.Column<string>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true),
                    KeyText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cultures", x => x.CultureId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(nullable: true),
                    BadEmail = table.Column<bool>(nullable: false),
                    NoEmail = table.Column<bool>(nullable: false),
                    NeedsVerification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    PhoneId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneType = table.Column<string>(nullable: true),
                    BadPhone = table.Column<bool>(nullable: false),
                    NoPhone = table.Column<bool>(nullable: false),
                    NeedsVerification = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.PhoneId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionKey = table.Column<string>(nullable: true),
                    QuestionType = table.Column<int>(nullable: false),
                    ConstraintKey = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "Registrants",
                columns: table => new
                {
                    RegistrantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: true),
                    EmailId = table.Column<int>(nullable: true),
                    PhoneId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrants", x => x.RegistrantId);
                    table.ForeignKey(
                        name: "FK_Registrants_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrants_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Registrants_Phones_PhoneId",
                        column: x => x.PhoneId,
                        principalTable: "Phones",
                        principalColumn: "PhoneId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    SurveyId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => new { x.QuestionId, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "AnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrantId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Registrants_RegistrantId",
                        column: x => x.RegistrantId,
                        principalTable: "Registrants",
                        principalColumn: "RegistrantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proxies",
                columns: table => new
                {
                    ProxyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrantId = table.Column<int>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proxies", x => x.ProxyId);
                    table.ForeignKey(
                        name: "FK_Proxies_Registrants_RegistrantId",
                        column: x => x.RegistrantId,
                        principalTable: "Registrants",
                        principalColumn: "RegistrantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Researchers",
                columns: table => new
                {
                    ResearcherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrantId = table.Column<int>(nullable: true),
                    InstitutionName = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Researchers", x => x.ResearcherId);
                    table.ForeignKey(
                        name: "FK_Researchers_Registrants_RegistrantId",
                        column: x => x.RegistrantId,
                        principalTable: "Registrants",
                        principalColumn: "RegistrantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientProxies",
                columns: table => new
                {
                    PatientId = table.Column<int>(nullable: false),
                    ProxyId = table.Column<int>(nullable: false),
                    Relationship = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProxies", x => new { x.PatientId, x.ProxyId });
                    table.ForeignKey(
                        name: "FK_PatientProxies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientProxies_Proxies_ProxyId",
                        column: x => x.ProxyId,
                        principalTable: "Proxies",
                        principalColumn: "ProxyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResearcherId = table.Column<int>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IRBNumber = table.Column<string>(nullable: true),
                    SurveyState = table.Column<string>(nullable: true),
                    SurveyDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.SurveyId);
                    table.ForeignKey(
                        name: "FK_Surveys_Researchers_ResearcherId",
                        column: x => x.ResearcherId,
                        principalTable: "Researchers",
                        principalColumn: "ResearcherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestions",
                columns: table => new
                {
                    SurveyId = table.Column<int>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestions", x => new { x.SurveyId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SurveyQuestions_Surveys_SurveyId",
                        column: x => x.SurveyId,
                        principalTable: "Surveys",
                        principalColumn: "SurveyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientProxies_ProxyId",
                table: "PatientProxies",
                column: "ProxyId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RegistrantId",
                table: "Patients",
                column: "RegistrantId");

            migrationBuilder.CreateIndex(
                name: "IX_Proxies_RegistrantId",
                table: "Proxies",
                column: "RegistrantId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_AnswerId",
                table: "QuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrants_AddressId",
                table: "Registrants",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrants_EmailId",
                table: "Registrants",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_Registrants_PhoneId",
                table: "Registrants",
                column: "PhoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Researchers_RegistrantId",
                table: "Researchers",
                column: "RegistrantId");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyQuestions_QuestionId",
                table: "SurveyQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ResearcherId",
                table: "Surveys",
                column: "ResearcherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cultures");

            migrationBuilder.DropTable(
                name: "PatientProxies");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "SurveyQuestions");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Proxies");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Surveys");

            migrationBuilder.DropTable(
                name: "Researchers");

            migrationBuilder.DropTable(
                name: "Registrants");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Phones");
        }
    }
}
