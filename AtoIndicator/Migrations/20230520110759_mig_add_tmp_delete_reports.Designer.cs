﻿// <auto-generated />
using System;
using AtoIndicator.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AtoIndicator.Migrations
{
    [DbContext(typeof(myDbContext))]
    [Migration("20230520110759_mig_add_tmp_delete_reports")]
    partial class mig_add_tmp_delete_reports
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AtoIndicator.DB.BasicInfo", b =>
                {
                    b.Property<DateTime>("생성시간")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("종목코드")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("거래량")
                        .HasColumnType("int");

                    b.Property<int>("고가")
                        .HasColumnType("int");

                    b.Property<double>("등락율")
                        .HasColumnType("double");

                    b.Property<long>("상장주식")
                        .HasColumnType("bigint");

                    b.Property<int>("상한가")
                        .HasColumnType("int");

                    b.Property<int>("시가")
                        .HasColumnType("int");

                    b.Property<long>("시가총액")
                        .HasColumnType("bigint");

                    b.Property<int>("연중최고")
                        .HasColumnType("int");

                    b.Property<int>("연중최저")
                        .HasColumnType("int");

                    b.Property<double>("외인소진률")
                        .HasColumnType("double");

                    b.Property<double>("유통비율")
                        .HasColumnType("double");

                    b.Property<long>("유통주식")
                        .HasColumnType("bigint");

                    b.Property<int>("저가")
                        .HasColumnType("int");

                    b.Property<int>("전일대비")
                        .HasColumnType("int");

                    b.Property<string>("종목명")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("종목타입")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("최고250")
                        .HasColumnType("int");

                    b.Property<double>("최고가250대비율")
                        .HasColumnType("double");

                    b.Property<string>("최고가250일")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("최저250")
                        .HasColumnType("int");

                    b.Property<double>("최저가250대비율")
                        .HasColumnType("double");

                    b.Property<string>("최저가250일")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("하한가")
                        .HasColumnType("int");

                    b.Property<int>("현재가")
                        .HasColumnType("int");

                    b.HasKey("생성시간", "종목코드");

                    b.ToTable("basicInfo");
                });

            modelBuilder.Entity("AtoIndicator.DB.LocationUser", b =>
                {
                    b.Property<string>("sUserName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("nUserLocationComp")
                        .HasColumnType("int");

                    b.HasKey("sUserName");

                    b.ToTable("locationUserDict");
                });

            modelBuilder.Entity("AtoIndicator.DB.StrategyNameDict", b =>
                {
                    b.Property<string>("sStrategyName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("nStrategyNameIdx")
                        .HasColumnType("int");

                    b.HasKey("sStrategyName");

                    b.HasIndex("nStrategyNameIdx")
                        .IsUnique();

                    b.ToTable("strategyNameDict");
                });
#pragma warning restore 612, 618
        }
    }
}
